using MediatR;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class GetServiceProviderReviewByIdQuery : IRequest<ServiceProviderReviewDto>
    {
        public string Id { get; set; }
    }
}
