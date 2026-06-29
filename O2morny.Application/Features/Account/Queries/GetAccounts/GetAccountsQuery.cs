using MediatR;

namespace O2morny.Application.Features.Account
{
    public class GetAccountsQuery : IRequest<List<AccountDto>>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
