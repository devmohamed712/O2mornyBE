using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class DeleteServiceProviderReviewCommandHandler : IRequestHandler<DeleteServiceProviderReviewCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteServiceProviderReviewCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteServiceProviderReviewCommand request, CancellationToken ct)
        {
            var serviceProviderReview = await _context.ServiceProviderReviews.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (serviceProviderReview == null)
                throw new NotFoundException("Service provider review not found");

            serviceProviderReview.IsDeleted = true;

            await _context.SaveChangesAsync(ct);
        }
    }
}
