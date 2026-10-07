using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;
using O2morny.Application.Common.Interfaces.Services;
using O2morny.Domain.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace O2morny.Application.Features.Shop
{
    public class UpdateShopCommandHandler : IRequestHandler<UpdateShopCommand, ShopDto>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IApplicationDbContext _context;
        private readonly IStorageService _storageService;

        public UpdateShopCommandHandler(
            IMapper mapper,
            IConfiguration configuration,
            IApplicationDbContext context,
            IStorageService storageService
            )
        {
            _context = context;
            _storageService = storageService;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ShopDto> Handle(UpdateShopCommand request, CancellationToken ct)
        {
            var shop = await _context.Shops.Include(x => x.WorkingHours).Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == request.Id && x.OwnerId == request.OwnerId && !x.IsDeleted, ct);

            if (shop == null)
                throw new NotFoundException("Shop not found.");

            // =========================
            // Validate City
            // =========================

            var cityExists = await _context.Cities.AnyAsync(x => x.Id == request.CityId && !x.IsDeleted, ct);

            if (!cityExists)
                throw new NotFoundException("City not found.");

            // =========================
            // Update Shop
            // =========================

            shop.Name = request.Name.Trim();
            shop.CityId = request.CityId;
            shop.Latitude = request.Latitude;
            shop.Longitude = request.Longitude;
            shop.UpdatedAt = DateTime.UtcNow;

            // =========================
            // Working Hours
            // =========================

            foreach (var workingHourDto in request.WorkingHours)
            {
                // Existing Working Hour
                if (workingHourDto.Id.HasValue)
                {
                    var existingWorkingHour = shop.WorkingHours.FirstOrDefault(x => x.Id == workingHourDto.Id.Value);

                    if (existingWorkingHour == null)
                    {
                        throw new ValidationException($"Working hour {workingHourDto.Id} does not belong to this shop.");
                    }

                    // Delete
                    if (workingHourDto.IsDeleted)
                    {
                        _context.ShopWorkingHours.Remove(existingWorkingHour);
                        continue;
                    }

                    // Update
                    existingWorkingHour.DayOfWeek = workingHourDto.DayOfWeek;

                    existingWorkingHour.OpenAt = workingHourDto.OpenAt;

                    existingWorkingHour.CloseAt = workingHourDto.CloseAt;
                }
                // New Working Hour
                else
                {
                    if (workingHourDto.IsDeleted)
                        continue;

                    shop.WorkingHours.Add(new ShopWorkingHour
                    {
                        ShopId = shop.Id,
                        DayOfWeek = workingHourDto.DayOfWeek,
                        OpenAt = workingHourDto.OpenAt,
                        CloseAt = workingHourDto.CloseAt
                    });
                }
            }

            // =========================
            // Images
            // =========================

            foreach (var imageDto in request.Images)
            {
                // Existing Image
                if (imageDto.Id.HasValue)
                {
                    var existingImage = shop.Images.FirstOrDefault(x => x.Id == imageDto.Id.Value);

                    if (existingImage == null)
                    {
                        throw new ValidationException($"Image {imageDto.Id} does not belong to this shop.");
                    }

                    // Delete
                    if (imageDto.IsDeleted)
                    {
                        var deletedImagePath = Path.Combine(_configuration["UploadedFiles:ShopsImages"]!, existingImage.Image);

                        _storageService.DeleteFile(deletedImagePath);

                        _context.ShopImages.Remove(existingImage);

                        continue;
                    }

                    // Update IsMain
                    existingImage.IsMain = imageDto.IsMain;
                }
                // New Image
                else
                {
                    if (imageDto.IsDeleted || imageDto.ImageFile == null)
                        continue;

                    var fileName = await _storageService.UploadFile(
                        imageDto.ImageFile.FileStream,
                        Path.GetExtension(imageDto.ImageFile.FileName).Trim('.'),
                        _configuration["UploadedFiles:ShopsImages"]!,
                        shop.Id.ToString());

                    shop.Images.Add(new ShopImage
                    {
                        ShopId = shop.Id,
                        Image = fileName,
                        IsMain = imageDto.IsMain
                    });
                }
            }

            // =========================
            // Save
            // =========================

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<ShopDto>(shop);
        }
    }
}
