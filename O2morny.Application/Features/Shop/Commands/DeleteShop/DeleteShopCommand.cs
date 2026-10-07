using MediatR;

namespace O2morny.Application.Features.Shop
{
    public class DeleteShopCommand : IRequest
    {
        public int Id { get; set; }
    }
}
