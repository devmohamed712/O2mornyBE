using MediatR;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class DeleteServiceProviderReviewCommand : IRequest
    {
        public int Id { get; set; }
    }
}
