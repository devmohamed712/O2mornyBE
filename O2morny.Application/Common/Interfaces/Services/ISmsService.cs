namespace O2morny.Application.Common.Interfaces.Services
{
    public interface ISmsService
    {
        Task<bool> SendAsync(string to, string message);
    }
}
