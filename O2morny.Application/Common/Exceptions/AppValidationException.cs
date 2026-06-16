
namespace O2morny.Application.Common.Exceptions
{
    public class AppValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; }

        public AppValidationException(Dictionary<string, string[]> errors)
        {
            Errors = errors;
        }
    }
}
