using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.Account
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto>
    {
        private readonly IApplicationDbContext _context;

        public GetAccountByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDto> Handle(GetAccountByIdQuery request, CancellationToken ct)
        {
            var account = await _context.Accounts
                .AsNoTracking()
                .Select(x => new AccountDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    NationalId = x.NationalId,
                    DateOfBirth = x.DateOfBirth,
                    HideBirthDate = x.HideBirthDate,
                    CityId = x.CityId,
                    Address = x.Address,
                    ProfilePicture = x.ProfilePicture,
                    NationalIdPicture = x.NationalIdPicture,
                    Status = x.Status
                })
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), ct);

            if (account == null)
                throw new Exception("Account not found");

            return account;
        }
    }
}
