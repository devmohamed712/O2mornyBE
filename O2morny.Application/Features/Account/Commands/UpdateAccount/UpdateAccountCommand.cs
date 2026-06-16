using MediatR;
using O2morny.Application.Common.Models;

namespace O2morny.Application.Features.Account
{
    public class UpdateAccountCommand : IRequest<AccountDto>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool HideBirthDate { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public FileModel? ProfilePictureFile { get; set; }
    }
}
