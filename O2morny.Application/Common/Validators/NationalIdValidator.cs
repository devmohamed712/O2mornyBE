using System.Text.RegularExpressions;

namespace O2morny.Application.Common.Validators
{
    public static class NationalIdValidator
    {
        public static bool BeValidEgyptianNationalId(string nationalId)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
                return false;

            if (!Regex.IsMatch(nationalId, @"^\d{14}$"))
                return false;

            var century = nationalId[0] switch
            {
                '2' => 1900,
                '3' => 2000,
                _ => -1
            };

            if (century == -1)
                return false;

            var year = int.Parse(nationalId.Substring(1, 2));
            var month = int.Parse(nationalId.Substring(3, 2));
            var day = int.Parse(nationalId.Substring(5, 2));

            try
            {
                var birthDate = new DateTime(
                    century + year,
                    month,
                    day);

                return birthDate <= DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }
    }
}