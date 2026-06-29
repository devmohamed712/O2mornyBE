using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class CreateServiceProviderReviewCommandHandler : IRequestHandler<CreateServiceProviderReviewCommand, ServiceProviderReviewDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateServiceProviderReviewCommandHandler(
            IMapper mapper,
            IApplicationDbContext context
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceProviderReviewDto> Handle(CreateServiceProviderReviewCommand request, CancellationToken ct)
        {
            var clientReviewExists = await _context.ServiceProviderReviews
                .AnyAsync(x => x.ServiceProviderProfileId == request.ServiceProviderProfileId &&
                x.ClientAccountId == request.ClientAccountId, ct);

            if (clientReviewExists)
                throw new BadRequestException("You have already reviewed this service provider.");

            var serviceProviderReview = new O2morny.Domain.Common.Entities.ServiceProviderReview
            {
                ServiceProviderProfileId = request.ServiceProviderProfileId,
                ClientAccountId = request.ClientAccountId,
                Rating = request.Rating,
                Review = request.Review,
                CreatedAt = DateTime.UtcNow,
            };

            await _context.ServiceProviderReviews.AddAsync(serviceProviderReview, ct);

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<ServiceProviderReviewDto>(serviceProviderReview);
        }
    }
}
