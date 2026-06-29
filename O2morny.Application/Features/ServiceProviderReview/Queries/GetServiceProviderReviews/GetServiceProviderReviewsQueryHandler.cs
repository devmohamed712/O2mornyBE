using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class GetServiceProviderReviewsQueryHandler : IRequestHandler<GetServiceProviderReviewsQuery, List<ServiceProviderReviewDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetServiceProviderReviewsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceProviderReviewDto>> Handle(GetServiceProviderReviewsQuery request, CancellationToken ct)
        {
            int skip = (request.Page - 1) * request.PageSize;

            var query = _context.ServiceProviderReviews
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.ServiceProviderProfileId))
            {
                query = query.Where(x => x.ServiceProviderProfileId == request.ServiceProviderProfileId);
            }

            if (!string.IsNullOrWhiteSpace(request.ClientAccountId))
            {
                query = query.Where(x => x.ClientAccountId == request.ClientAccountId);
            }

            return await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skip)
                .Take(request.PageSize)
                .Select(x => new ServiceProviderReviewDto
                {
                    Id = x.Id,
                    ServiceProviderProfileId = x.ServiceProviderProfileId,
                    ClientAccountId = x.ClientAccountId,
                    Rating = x.Rating,
                    Review = x.Review,
                    CreatedAt = x.CreatedAt,
                })
                .ToListAsync(ct);
        }
    }
}
