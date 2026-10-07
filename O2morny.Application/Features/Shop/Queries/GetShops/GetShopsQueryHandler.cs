using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.Shop
{
    internal class GetShopsQueryHandler : IRequestHandler<GetShopsQuery, List<ShopDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetShopsQueryHandler(
            IMapper mapper,
            IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<ShopDto>> Handle(GetShopsQuery request, CancellationToken ct)
        {
            int skip = (request.Page - 1) * request.PageSize;

            return await _context.Shops
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Skip(skip)
                .Take(request.PageSize)
                .Select(x => new ShopDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    OwnerId = x.OwnerId,
                    CityId = x.CityId,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    ShopImages = _mapper.Map<List<ShopImageDto>>(x.Images),
                    ShopWorkingHours = _mapper.Map<List<ShopWorkingHourDto>>(x.WorkingHours),
                })
                .ToListAsync(ct);
        }
    }
}
