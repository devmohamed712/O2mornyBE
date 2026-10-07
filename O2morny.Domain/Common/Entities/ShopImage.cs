using O2morny.Domain.Common.Interfaces;

namespace O2morny.Domain.Common.Entities
{
    public class ShopImage : IEntity<int>
    {
        public int Id { get; set; }

        public int ShopId { get; set; }

        public string Image { get; set; }

        public bool IsMain { get; set; }


        public Shop Shop { get; set; }
    }
}
