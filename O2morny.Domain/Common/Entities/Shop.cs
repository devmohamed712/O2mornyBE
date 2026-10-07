using O2morny.Domain.Common.Interfaces;

namespace O2morny.Domain.Common.Entities
{
    public class Shop : IEntity<int>, IAuditable, ISoftDelete
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public string Name { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int CityId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }


        public Account Owner { get; set; }
        public City City { get; set; }
        public ICollection<ShopImage> Images { get; set; } = new List<ShopImage>();
        public ICollection<ShopWorkingHour> WorkingHours { get; set; } = new List<ShopWorkingHour>();
    }
}
