using AutoMapper;
using MediatR;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;
using System.Data.Entity;

namespace O2morny.Application.Features.Country
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, CountryDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateCountryCommandHandler(
            IMapper mapper,
            IApplicationDbContext context
        )
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CountryDto> Handle(UpdateCountryCommand request, CancellationToken ct)
        {
            var country = await _context.Countries
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (country is null)
                throw new NotFoundException("Country not found");

            var countryExists = await _context.Countries.AnyAsync(x =>
                x.Id != request.Id &&
                (x.EnName.ToLower().Trim() == request.EnName.ToLower().Trim() &&
                x.ArName.ToLower().Trim() == request.ArName.ToLower().Trim()),
                ct);

            if (countryExists)
                throw new BadRequestException("Country already exists");

            country.EnName = request.EnName.Trim();
            country.ArName = request.ArName.Trim();

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<CountryDto>(country);
        }
    }
}
