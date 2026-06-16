using MediatR;
using O2morny.Application.Features.Account;

namespace O2morny.Application.Features.Account
{
    public class GetAccountsQuery : IRequest<List<AccountDto>>
    {
    }
}
