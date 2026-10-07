using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;
using O2morny.Application.Common.Interfaces.Services;
using O2morny.Domain.Common.Entities;

namespace O2morny.Application.Features.Shop
{
    public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand, ShopDto>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IApplicationDbContext _context;
        private readonly IStorageService _storageService;

        public CreateShopCommandHandler(
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

        public async Task<ShopDto> Handle(CreateShopCommand request, CancellationToken ct)
        {
            var cityExists = await _context.Cities.AnyAsync(x => x.Id == request.CityId && !x.IsDeleted, ct);

            if (!cityExists)
                throw new NotFoundException("City not found.");

            var shop = new O2morny.Domain.Common.Entities.Shop
            {
                OwnerId = request.OwnerId,
                Name = request.Name.Trim(),
                CityId = request.CityId,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };

            foreach (var workingHour in request.WorkingHours)
            {
                shop.WorkingHours.Add(new ShopWorkingHour
                {
                    DayOfWeek = workingHour.DayOfWeek,
                    OpenAt = workingHour.OpenAt,
                    CloseAt = workingHour.CloseAt
                });
            }

            await _context.Shops.AddAsync(shop, ct);

            await _context.SaveChangesAsync(ct);

            foreach (var image in request.Images)
            {
                var fileName = await _storageService.UploadFile(image.ImageFile.FileStream, Path.GetExtension(image.ImageFile.FileName).Trim('.'), _configuration["UploadedFiles:ShopsImages"]!, shop.Id.ToString());

                shop.Images.Add(new ShopImage
                {
                    Image = fileName,
                    IsMain = image.IsMain
                });
            }

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<ShopDto>(shop);
        }
    }
}
