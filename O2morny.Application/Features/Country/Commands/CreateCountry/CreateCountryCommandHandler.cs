using AutoMapper;
using MediatR;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;
using System.Data.Entity;

namespace O2morny.Application.Features.Country
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CountryDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateCountryCommandHandler(
            IMapper mapper,
            IApplicationDbContext context
        )
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CountryDto> Handle(CreateCountryCommand request, CancellationToken ct)
        {
            var countryExists = await _context.Countries.AnyAsync(x =>
            x.EnName.ToLower().Trim().Equals(request.EnName.ToLower().Trim()) ||
            x.ArName.ToLower().Trim().Equals(request.ArName.ToLower().Trim()),
            ct);

            if (countryExists)
                throw new BadRequestException("Country already exists");

            var country = new Domain.Common.Entities.Country
            {
                EnName = request.EnName.Trim(),
                ArName = request.ArName.Trim()
            };

            await _context.Countries.AddAsync(country, ct);

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<CountryDto>(country);
        }
    }
}
