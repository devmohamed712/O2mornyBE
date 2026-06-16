using AutoMapper;
using MediatR;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;
using System.Data.Entity;

namespace O2morny.Application.Features.City
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, CityDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateCityCommandHandler(
            IMapper mapper,
            IApplicationDbContext context
        )
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CityDto> Handle(UpdateCityCommand request, CancellationToken ct)
        {
            var city = await _context.Cities
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (city is null)
                throw new NotFoundException("City not found");

            var cityExists = await _context.Cities.AnyAsync(x =>
                x.Id != request.Id &&
                x.CountryId == request.CountryId &&
                (x.EnName.ToLower().Trim() == request.EnName.ToLower().Trim() ||
                x.ArName.ToLower().Trim() == request.ArName.ToLower().Trim()),
                ct);

            if (cityExists)
                throw new BadRequestException("City already exists");

            city.EnName = request.EnName.Trim();
            city.ArName = request.ArName.Trim();
            city.CountryId = request.CountryId;

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<CityDto>(city);
        }
    }
}
