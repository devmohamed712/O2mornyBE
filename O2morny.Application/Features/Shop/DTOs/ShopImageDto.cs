using O2morny.Application.Common.Models;

namespace O2morny.Application.Features.Shop
{
    public class ShopImageDto
    {
        public int? Id { get; set; }

        public int ShopId { get; set; }

        public string? Image { get; set; }

        public bool IsMain { get; set; }

        public bool IsDeleted { get; set; }

        public FileModel? ImageFile { get; set; }
    }
}
