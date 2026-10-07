using O2morny.Domain.Common.Enums;

namespace O2morny.Application.Features.Account
{
    public class AccountDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public string ProfilePicture { get; set; }
    }
}
