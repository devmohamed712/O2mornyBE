using PhoneNumbers;

public static class PhoneNormalizer
{
    private static readonly PhoneNumberUtil _util = PhoneNumberUtil.GetInstance();

    public static string? Normalize(string rawInput, string defaultRegion = "EG")
    {
        try
        {
            var parsed = _util.Parse(rawInput, defaultRegion);

            if (!_util.IsValidNumber(parsed))
                return null;

            return _util.Format(parsed, PhoneNumberFormat.E164);
        }
        catch (NumberParseException)
        {
            return null;
        }
    }
}