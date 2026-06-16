using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;
using O2morny.Application.Features.Account;

namespace O2morny.Application.Features.Account
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<AccountDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAccountsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AccountDto>> Handle(GetAccountsQuery request, CancellationToken ct)
        {
            return await _context.Accounts
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
                .ToListAsync(ct);
        }
    }
}
