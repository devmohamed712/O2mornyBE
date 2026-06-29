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
                    NationalId = x.NationalId,
                    DateOfBirth = x.DateOfBirth,
                    HideBirthDate = x.HideBirthDate,
                    CityId = x.CityId,
                    Address = x.Address,
                    ProfilePicture = x.ProfilePicture,
                    NationalIdPicture = x.NationalIdPicture,
                    Status = x.Status,
                    ServiceProviderExperienceYears = x.ServiceProviderProfile != null
                        ? x.ServiceProviderProfile.ExperienceYears
                        : null,
                    ServiceProviderDescription = x.ServiceProviderProfile != null
                        ? x.ServiceProviderProfile.Description
                        : null
                })
                .ToListAsync(ct);
        }
    }
}
