
namespace O2morny.Application.Common.Interfaces.Services
{
    public interface IWhatsappService
    {
        Task SendOtp(string phone, string otp);
    }
}
