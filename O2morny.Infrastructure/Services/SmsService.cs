using Microsoft.Extensions.Configuration;
using O2morny.Application.Common.Interfaces.Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace O2morny.Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<bool> SendAsync(string to, string message)
        {
            try
            {
                TwilioClient.Init(_configuration["SendSMS:AccountSID"], _configuration["SendSMS:AuthToken"]);
                await MessageResource.CreateAsync(body: message, from: _configuration["SendSMS:SmsSender"], to: to);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
