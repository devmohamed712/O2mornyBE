using Microsoft.AspNetCore.Identity;

namespace O2morny.Infrastructure.Persistence.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public string EnName { get; set; }

        public string ArName { get; set; }
    }
}
