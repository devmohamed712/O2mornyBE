
namespace O2morny.Application.Features
{
    public class ServiceProviderReviewDto
    {
        public int Id { get; set; }

        public string ServiceProviderProfileId { get; set; }

        public string ClientAccountId { get; set; }

        public decimal Rating { get; set; }

        public string Review { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
