
namespace O2morny.Application.Common.Models
{
    public class JwtUserModel
    {
        public string Id { get; set; }

        public string PhoneNumber { get; set; }

        public IList<string> Roles { get; set; } = [];
    }
}
