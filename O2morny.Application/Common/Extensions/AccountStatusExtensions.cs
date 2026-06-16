using O2morny.Domain.Common.Enums;

namespace O2morny.Application.Common.Extensions
{
    public static class AccountStatusExtensions
    {
        public static bool CanLogin(this AccountStatus status)
        {
            return status == AccountStatus.Active
                || status == AccountStatus.Pending
                || status == AccountStatus.UnderReview;
        }
    }
}
