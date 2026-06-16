using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using O2morny.Application.Common.Interfaces.Services;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace O2morny.Infrastructure.Services
{
    public class UtilitiesService : IUtilitiesService
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        public UtilitiesService(LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _linkGenerator = linkGenerator;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }


        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public string RandomNumber()
        {
            StringBuilder build = new StringBuilder();
            Random rng = new Random();
            for (int i = 0; i < 6; i++)
            {
                build.Append(rng.Next(0, 9));
            }
            string random = build.ToString();
            random = "0" + random + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            return random;
        }

        public object GetPropertyValue<T>(T obj, string propName)
        {
            var site = System.Runtime.CompilerServices.CallSite<Func<System.Runtime.CompilerServices.CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(0, propName, obj.GetType(), new[] { Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(0, null) }));
            var val = site.Target(site, obj);
            return val;
        }

        public PropertyInfo GetProperty<T>(T obj, string propName)
        {
            PropertyInfo prop = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
            return prop;
        }

        public object GetProperty(object target, string name)
        {
            var site = System.Runtime.CompilerServices.CallSite<Func<System.Runtime.CompilerServices.CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(0, name, target.GetType(), new[] { Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(0, null) }));
            return site.Target(site, target);
        }

        public string ToTitleCase(string word)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.ToLower());
        }

        public string GenerateApiLink(string action, string controller, object values)
        {
            var httpContext = _contextAccessor.HttpContext;
            if (httpContext == null)
                throw new InvalidOperationException("HttpContext is not available");

            return _linkGenerator.GetUriByAction(httpContext, action, controller, values);
        }

        public string GenerateFrontendLink(string path, object query)
        {
            var baseUrl = _configuration["Pages:PasswordResetLink"];

            var queryString = QueryString.Create(query.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(query)?.ToString()));

            return $"{baseUrl}/{path}{queryString}";
        }

        public T AddPrecentageToValue<T>(T precentageValue, T value)
        {
            dynamic preValue = precentageValue;
            dynamic constantValue = value;
            T result = decimal.Multiply(constantValue ?? 0, preValue ?? 0) / 100;
            return result;
        }
    }
}
