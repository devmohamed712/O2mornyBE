using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.City
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, List<CityDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCitiesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CityDto>> Handle(GetCitiesQuery request, CancellationToken ct)
        {
            return await _context.Cities
                .AsNoTracking()
                .Where(x => x.CountryId == request.CountryId)
                .Select(x => new CityDto
                {
                    Id = x.Id,
                    EnName = x.EnName,
                    ArName = x.ArName,
                    CountryId = x.CountryId
                })
                .ToListAsync(ct);
        }
    }
}
