using MediatR;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class UpdateServiceProviderReviewCommand : IRequest<ServiceProviderReviewDto>
    {
        public int Id { get; set; }

        public decimal Rating { get; set; }

        public string Review { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
