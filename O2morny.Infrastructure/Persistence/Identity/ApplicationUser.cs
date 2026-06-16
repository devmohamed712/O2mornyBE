using Microsoft.AspNetCore.Identity;
using O2morny.Domain.Common.Entities;

namespace O2morny.Infrastructure.Persistence.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Account Account { get; set; }
    }
}
