using MediatR;

namespace O2morny.Application.Features.Shop
{
    public class GetShopByIdQuery : IRequest<ShopDto>
    {
        public int Id { get; set; }
    }
}
