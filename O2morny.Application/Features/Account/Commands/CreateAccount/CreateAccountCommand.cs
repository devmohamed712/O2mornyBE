using MediatR;
using O2morny.Application.Common.Models;

namespace O2morny.Application.Features.Account
{
    public class CreateAccountCommand : IRequest<AccountDto>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public bool IsAcceptTerms { get; set; }

        public bool IsAcceptPrivacy { get; set; }

        public FileModel? ProfilePictureFile { get; set; }
    }
}
