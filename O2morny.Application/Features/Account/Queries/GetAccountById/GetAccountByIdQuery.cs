using MediatR;

namespace O2morny.Application.Features.Account
{
    public class GetAccountByIdQuery : IRequest<AccountDto>
    {
        public string Id { get; set; }
    }
}

