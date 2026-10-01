using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;
using O2morny.Application.Common.Interfaces.Services;
using O2morny.Domain.Common.Enums;

namespace O2morny.Application.Features.Account
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IApplicationDbContext _context;
        private readonly IStorageService _storageService;


        public CreateAccountCommandHandler(
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

        public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken ct)
        {
            var cityExists = await _context.Cities
                .AnyAsync(x => x.Id == request.CityId, ct);

            if (!cityExists)
                throw new NotFoundException("City not found");

            var account = new O2morny.Domain.Common.Entities.Account
            {
                Id = request.Id,
                Name = request.Name.Trim(),
                DateOfBirth = request.DateOfBirth,
                CityId = request.CityId,
                Address = request.Address.Trim(),
                IsAcceptTerms = request.IsAcceptTerms,
                IsAcceptPrivacy = request.IsAcceptPrivacy,
                Status = AccountStatus.Active,
            };


            if (request.ProfilePictureFile != null)
            {
                string image = await _storageService.UploadFile(request.ProfilePictureFile.FileStream, Path.GetExtension(request.ProfilePictureFile.FileName).Trim('.'), _configuration["UploadedFiles:ProfilesImages"]!, request.Id);

                account.ProfilePicture = image;
            }

            await _context.Accounts.AddAsync(account, ct);

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<AccountDto>(account);
        }
    }
}
