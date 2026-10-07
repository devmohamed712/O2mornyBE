using MediatR;

namespace O2morny.Application.Features.Shop
{
    public class UpdateShopCommand : IRequest<ShopDto>
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public List<ShopWorkingHourDto> WorkingHours { get; set; } = new();

        public List<ShopImageDto> Images { get; set; } = new();
    }
}
