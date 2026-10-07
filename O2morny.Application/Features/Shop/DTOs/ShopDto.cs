namespace O2morny.Application.Features.Shop
{
    public class ShopDto
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public string Name { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int CityId { get; set; }


        public List<ShopImageDto> ShopImages { get; set; }
        public List<ShopWorkingHourDto> ShopWorkingHours { get; set; }
    }
}
