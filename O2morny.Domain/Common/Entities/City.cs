using O2morny.Domain.Common.Interfaces;

namespace O2morny.Domain.Common.Entities
{
    public class City : IEntity<int>, ISoftDelete
    {
        public int Id { get; set; }

        public string ArName { get; set; }

        public string EnName { get; set; }

        public int CountryId { get; set; }

        public bool IsDeleted { get; set; }


        public Country Country { get; set; }
        public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
    }
}
