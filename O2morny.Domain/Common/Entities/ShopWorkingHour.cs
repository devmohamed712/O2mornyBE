using O2morny.Domain.Common.Interfaces;

namespace O2morny.Domain.Common.Entities
{
    public class ShopWorkingHour : IEntity<int>, IAuditable
    {
        public int Id { get; set; }

        public int ShopId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan OpenAt { get; set; }

        public TimeSpan CloseAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


        public Shop Shop { get; set; }
    }
}
