using O2morny.Domain.Common.Interfaces;

namespace O2morny.Domain.Common.Entities
{
    public class Country : IEntity<int>, ISoftDelete
    {
        public int Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public bool IsDeleted { get; set; }


        public ICollection<City> Cities { get; set; } = new HashSet<City>();
    }
}
