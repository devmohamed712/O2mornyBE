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
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IApplicationDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IAuthService _authService;

        public CreateAccountCommandHandler(
            IMapper mapper,
            IConfiguration configuration,
            IApplicationDbContext context,
            IStorageService storageService,
            IAuthService authService
            )
        {
            _context = context;
            _storageService = storageService;
            _configuration = configuration;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken ct)
        {
            var nationalIdExists = await _context.Accounts.AnyAsync(x => x.NationalId == request.NationalId, ct);

            if (nationalIdExists)
                throw new BadRequestException("National ID already exists");

            var cityExists = await _context.Cities
                .AnyAsync(x => x.Id == request.CityId, ct);

            if (!cityExists)
                throw new NotFoundException("City not found");

            var account = new O2morny.Domain.Common.Entities.Account
            {
                Id = request.Id,
                Name = request.Name.Trim(),
                NationalId = request.NationalId.Trim(),
                DateOfBirth = request.DateOfBirth,
                HideBirthDate = request.HideBirthDate,
                CityId = request.CityId,
                Address = request.Address.Trim(),
                IsAcceptTerms = request.IsAcceptTerms,
                IsAcceptPrivacy = request.IsAcceptPrivacy,
                Status = AccountStatus.Pending,
                ServiceProviderProfile = request.Role == nameof(AccountRole.ServiceProvider) ? new ServiceProviderProfile
                {
                    ExperienceYears = request.ServiceProviderExperienceYears!.Value,
                    Description = request.ServiceProviderDescription!.Trim()
                } : null
            };

            if (request.ProfilePictureFile != null || request.NationalIdPictureFile != null)
            {
                if (request.ProfilePictureFile != null)
                {
                    string image = await _storageService.UploadFile(request.ProfilePictureFile.FileStream, Path.GetExtension(request.ProfilePictureFile.FileName).Trim('.'), _configuration["UploadedFiles:ProfilesImages"]!, request.Id);

                    account.ProfilePicture = image;
                }

                if (request.NationalIdPictureFile != null)
                {
                    string image = await _storageService.UploadFile(request.NationalIdPictureFile.FileStream, Path.GetExtension(request.NationalIdPictureFile.FileName).Trim('.'), _configuration["UploadedFiles:NationalIdsImages"]!, request.Id);

                    account.NationalIdPicture = image;
                }
            }

            await _context.Accounts.AddAsync(account, ct);

            await _context.SaveChangesAsync(ct);

            await _authService.AssignRoleAsync(account.Id, request.Role);

            return _mapper.Map<AccountDto>(account);
        }
    }
}
