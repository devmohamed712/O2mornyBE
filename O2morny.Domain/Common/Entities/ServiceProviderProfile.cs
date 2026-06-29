
namespace O2morny.Domain.Common.Entities
{
    public class ServiceProviderProfile
    {
        public string AccountId { get; set; }

        public decimal ExperienceYears { get; set; }

        public string Description { get; set; }


        public Account Account { get; set; }
        public ICollection<ServiceProviderReview> ReceivedReviews { get; set; } = [];
    }
}
