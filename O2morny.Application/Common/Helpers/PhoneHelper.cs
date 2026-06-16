
namespace O2morny.Application.Common.Helpers
{
    public static class PhoneHelper
    {
        public static string Normalize(string phone)
        {
            phone = phone.Replace(" ", "");

            if (phone.StartsWith("+"))
                phone = phone.Substring(1);

            if (phone.StartsWith("002"))
                phone = phone.Substring(2);

            if (phone.StartsWith("01"))
                phone = "2" + phone;

            return phone;
        }
    }
}
