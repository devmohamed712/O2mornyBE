using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.Country
{
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, CountryDto>
    {
        private readonly IApplicationDbContext _context;

        public GetCountryByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CountryDto> Handle(GetCountryByIdQuery request, CancellationToken ct)
        {
            var country = await _context.Countries
                .AsNoTracking()
                .Select(x => new CountryDto
                {
                    Id = x.Id,
                    EnName = x.EnName,
                    ArName = x.ArName,
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (country == null)
                throw new Exception("Country not found");

            return country;
        }
    }
}
