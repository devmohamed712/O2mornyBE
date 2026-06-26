using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;
using O2morny.Application.Common.Interfaces.Services;
using O2morny.Domain.Common.Entities;
using O2morny.Domain.Common.Enums;

namespace O2morny.Application.Features.Account
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountDto>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IApplicationDbContext _context;
        private readonly IStorageService _storageService;

        public UpdateAccountCommandHandler(
            IMapper mapper,
            IConfiguration configuration,
            IApplicationDbContext context,
            IStorageService storageService
            )
        {
            _context = context;
            _storageService = storageService;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<AccountDto> Handle(UpdateAccountCommand request, CancellationToken ct)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (account == null)
                throw new NotFoundException("Account not found");

            var cityExists = await _context.Cities.AnyAsync(x => x.Id == request.CityId, ct);

            if (!cityExists)
                throw new NotFoundException("City not found");

            account.Name = request.Name.Trim();
            account.DateOfBirth = request.DateOfBirth;
            account.HideBirthDate = request.HideBirthDate;
            account.CityId = request.CityId;
            account.Address = request.Address.Trim();
            account.UpdatedAt = DateTime.UtcNow;
            account.ServiceProviderProfile = request.Role == nameof(AccountRole.ServiceProvider) ? new ServiceProviderProfile
            {
                ExperienceYears = request.ServiceProviderExperienceYears!.Value,
                Description = request.ServiceProviderDescription!.Trim()
            } : null;

            if (request.ProfilePictureFile?.FileStream != null)
            {
                string image = await _storageService.UploadFile(
                    request.ProfilePictureFile.FileStream,
                    Path.GetExtension(request.ProfilePictureFile.FileName).Trim('.'),
                    _configuration["UploadedFiles:ProfilesImages"]!,
                    account.Id);

                account.ProfilePicture = image;
            }

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<AccountDto>(account);
        }
    }
}
