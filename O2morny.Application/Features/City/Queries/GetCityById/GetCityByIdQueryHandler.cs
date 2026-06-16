using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.City
{
    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, CityDto>
    {
        private readonly IApplicationDbContext _context;

        public GetCityByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CityDto> Handle(GetCityByIdQuery request, CancellationToken ct)
        {
            var city = await _context.Cities
                .AsNoTracking()
                .Select(x => new CityDto
                {
                    Id = x.Id,
                    EnName = x.EnName,
                    ArName = x.ArName,
                    CountryId = x.CountryId
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (city == null)
                throw new Exception("City not found");

            return city;
        }
    }
}
