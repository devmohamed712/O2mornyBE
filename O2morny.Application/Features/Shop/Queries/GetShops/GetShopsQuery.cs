using MediatR;

namespace O2morny.Application.Features.Shop
{
    public class GetShopsQuery : IRequest<List<ShopDto>>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
