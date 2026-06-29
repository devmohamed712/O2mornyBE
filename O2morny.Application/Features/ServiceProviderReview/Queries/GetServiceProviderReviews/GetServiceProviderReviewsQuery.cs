using MediatR;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class GetServiceProviderReviewsQuery : IRequest<List<ServiceProviderReviewDto>>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string? ServiceProviderProfileId { get; set; }

        public string? ClientAccountId { get; set; }
    }
}
