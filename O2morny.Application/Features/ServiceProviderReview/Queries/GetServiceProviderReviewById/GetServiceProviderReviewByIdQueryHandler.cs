using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class GetServiceProviderReviewByIdQueryHandler : IRequestHandler<GetServiceProviderReviewByIdQuery, ServiceProviderReviewDto>
    {
        private readonly IApplicationDbContext _context;

        public GetServiceProviderReviewByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceProviderReviewDto> Handle(GetServiceProviderReviewByIdQuery request, CancellationToken ct)
        {
            var serviceProviderReview = await _context.ServiceProviderReviews
                .AsNoTracking()
                .Select(x => new ServiceProviderReviewDto
                {
                    Id = x.Id,
                    ServiceProviderProfileId = x.ServiceProviderProfileId,
                    ClientAccountId = x.ClientAccountId,
                    Rating = x.Rating,
                    Review = x.Review,
                    CreatedAt = x.CreatedAt,
                })
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), ct);

            if (serviceProviderReview == null)
                throw new Exception("Service provider review not found");

            return serviceProviderReview;
        }
    }
}
