
using O2morny.Domain.Common.Enums;

namespace O2morny.Application.Features.Account
{
    public class AccountDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string NationalId { get; set; }

        public string NationalIdPicture { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool HideBirthDate { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public string ProfilePicture { get; set; }

        public AccountStatus Status { get; set; }
    }
}
