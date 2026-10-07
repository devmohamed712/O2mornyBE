using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;

namespace O2morny.Application.Features.Shop
{
    public class DeleteShopCommandHandler : IRequestHandler<DeleteShopCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteShopCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteShopCommand request, CancellationToken ct)
        {
            var shop = await _context.Shops.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (shop == null)
                throw new NotFoundException("Shop not found");

            shop.IsDeleted = true;

            await _context.SaveChangesAsync(ct);
        }
    }
}
