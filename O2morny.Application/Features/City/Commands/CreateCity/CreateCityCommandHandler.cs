using AutoMapper;
using MediatR;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;
using System.Data.Entity;

namespace O2morny.Application.Features.City
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateCityCommandHandler(
            IMapper mapper,
            IApplicationDbContext context
        )
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CityDto> Handle(CreateCityCommand request, CancellationToken ct)
        {
            var cityExists = await _context.Cities.AnyAsync(x =>
            x.CountryId == request.CountryId &&
            (x.EnName.ToLower().Trim().Equals(request.EnName.ToLower().Trim()) ||
            x.ArName.ToLower().Trim().Equals(request.ArName.ToLower().Trim())),
            ct);

            if (cityExists)
                throw new BadRequestException("City already exists");

            var city = new Domain.Common.Entities.City
            {
                EnName = request.EnName.Trim(),
                ArName = request.ArName.Trim(),
                CountryId = request.CountryId
            };

            await _context.Cities.AddAsync(city, ct);

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<CityDto>(city);
        }
    }
}
