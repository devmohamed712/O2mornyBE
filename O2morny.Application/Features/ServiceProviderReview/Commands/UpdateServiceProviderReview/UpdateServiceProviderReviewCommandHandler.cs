using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class UpdateServiceProviderReviewCommandHandler : IRequestHandler<UpdateServiceProviderReviewCommand, ServiceProviderReviewDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateServiceProviderReviewCommandHandler(
            IMapper mapper,
            IApplicationDbContext context
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceProviderReviewDto> Handle(UpdateServiceProviderReviewCommand request, CancellationToken ct)
        {
            var serviceProviderReview = await _context.ServiceProviderReviews.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (serviceProviderReview == null)
                throw new NotFoundException("Service provider review not found");

            serviceProviderReview.Rating = request.Rating;
            serviceProviderReview.Review = request.Review;
            serviceProviderReview.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<ServiceProviderReviewDto>(serviceProviderReview);
        }
    }
}
