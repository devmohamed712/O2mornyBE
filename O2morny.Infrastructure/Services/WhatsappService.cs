using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using O2morny.Application.Common.Interfaces.Services;
using System.Net.Http.Headers;
using System.Text;

namespace O2morny.Infrastructure.Services
{
    public class WhatsappService : IWhatsappService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WhatsappService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendOtp(string phone, string otp)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _configuration["WhatsApp:ApiKey"]);

            var body = new
            {
                session_id = _configuration["WhatsApp:SessionId"],
                to = phone,
                message = $"Your OTP code is: {otp}"
            };

            var json = JsonConvert.SerializeObject(body);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(
                _configuration["WhatsApp:Url"],
                content
            );

            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }
        }
    }
}
