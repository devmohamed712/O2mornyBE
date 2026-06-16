namespace O2morny.Application.Common.Interfaces.Identity
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }

        string? Role { get; }

        bool IsAuthenticated { get; }

        bool IsInRole(string role);
    }
}
