using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Persistence;
using O2morny.Domain.Common.Enums;

namespace O2morny.Application.Features.Account
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteAccountCommand request, CancellationToken ct)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (account == null)
                throw new NotFoundException("Account not found");

            account.Status = AccountStatus.DeletedByUser;
            account.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
        }
    }
}
