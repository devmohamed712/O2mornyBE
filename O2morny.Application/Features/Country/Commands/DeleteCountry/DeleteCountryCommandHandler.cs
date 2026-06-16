using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.Country
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCountryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCountryCommand request, CancellationToken ct)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (country == null)
                throw new NotFoundException("Country not found");

            country.IsDeleted = true;

            await _context.SaveChangesAsync(ct);
        }
    }
}
