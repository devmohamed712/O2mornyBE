using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.Country
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<CountryDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCountriesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CountryDto>> Handle(GetCountriesQuery request, CancellationToken ct)
        {
            return await _context.Countries
                .AsNoTracking()
                .Select(x => new CountryDto
                {
                    Id = x.Id,
                    EnName = x.EnName,
                    ArName = x.ArName,
                })
                .ToListAsync(ct);
        }
    }
}
