using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.City
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCityCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCityCommand request, CancellationToken ct)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (city == null)
                throw new NotFoundException("City not found");

            city.IsDeleted = true;

            await _context.SaveChangesAsync(ct);
        }
    }
}
