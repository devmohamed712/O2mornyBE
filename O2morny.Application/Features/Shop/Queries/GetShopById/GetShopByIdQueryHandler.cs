using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.Shop
{
    public class GetShopByIdQueryHandler : IRequestHandler<GetShopByIdQuery, ShopDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetShopByIdQueryHandler(
            IMapper mapper, 
            IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ShopDto> Handle(GetShopByIdQuery request, CancellationToken ct)
        {
            var shop = await _context.Shops
                .AsNoTracking()
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
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (shop == null)
                throw new Exception("Shop not found");

            return shop;
        }
    }
}
