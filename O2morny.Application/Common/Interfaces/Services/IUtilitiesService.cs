using System.Reflection;

namespace O2morny.Application.Common.Interfaces.Services
{
    public interface IUtilitiesService
    {
        string RandomString(int size, bool lowerCase);

        string RandomNumber();

        object GetPropertyValue<T>(T obj, string propName);

        PropertyInfo GetProperty<T>(T obj, string propName);

        object GetProperty(object target, string name);

        string ToTitleCase(string word);

        string GenerateApiLink(string action, string controller, object values);

        string GenerateFrontendLink(string path, object query);

        T AddPrecentageToValue<T>(T precentageValue, T value);
    }
}
