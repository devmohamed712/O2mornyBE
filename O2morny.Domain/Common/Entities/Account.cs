using O2morny.Domain.Common.Enums;
using O2morny.Domain.Common.Interfaces;

namespace O2morny.Domain.Common.Entities
{
    public class Account : IEntity<string>, IAuditable
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool HideBirthDate { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public string NationalId { get; set; }

        public string? NationalIdPicture { get; set; }

        public string? ProfilePicture { get; set; }

        public bool IsAcceptTerms { get; set; }

        public bool IsAcceptPrivacy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public AccountStatus Status { get; set; }


        public City City { get; set; }
    }
}
