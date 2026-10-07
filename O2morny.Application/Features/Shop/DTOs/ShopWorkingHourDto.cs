namespace O2morny.Application.Features.Shop
{
    public class ShopWorkingHourDto
    {
        public int? Id { get; set; }

        public int? ShopId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan OpenAt { get; set; }

        public TimeSpan CloseAt { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
