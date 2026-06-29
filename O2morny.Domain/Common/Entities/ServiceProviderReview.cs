using O2morny.Domain.Common.Enums;
using O2morny.Domain.Common.Interfaces;

namespace O2morny.Domain.Common.Entities
{
    public class ServiceProviderReview : IEntity<int>, IAuditable, ISoftDelete
    {
        public int Id { get; set; }

        public string ServiceProviderProfileId { get; set; }

        public string ClientAccountId { get; set; }

        public decimal Rating { get; set; }

        public string Review { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }


        public ServiceProviderProfile ServiceProviderProfile { get; set; }
        public Account ClientAccount { get; set; }
    }
}
