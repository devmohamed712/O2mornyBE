using MediatR;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class CreateServiceProviderReviewCommand : IRequest<ServiceProviderReviewDto>
    {
        public string ServiceProviderProfileId { get; set; }

        public string ClientAccountId { get; set; }

        public decimal Rating { get; set; }

        public string Review { get; set; }
    }
}
