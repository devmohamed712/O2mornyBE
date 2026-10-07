using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;

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
            int skip = (request.Page - 1) * request.PageSize;

            return await _context.Accounts
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Skip(skip)
                .Take(request.PageSize)
                .Select(x => new AccountDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    DateOfBirth = x.DateOfBirth,
                    CountryId = x.City.CountryId,
                    CityId = x.CityId,
                    Address = x.Address,
                    ProfilePicture = x.ProfilePicture,
                })
                .ToListAsync(ct);
        }
    }
}
