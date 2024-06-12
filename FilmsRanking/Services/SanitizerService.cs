using FilmsRanking.Interfaces;
using Ganss.Xss;
using System.Text.RegularExpressions;

namespace FilmsRanking.Services
{
    public class SanitizerService : ISanitizerService
    {
        private static readonly HtmlSanitizer _htmlSanitizer = new HtmlSanitizer();

        public string SanitizePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return phoneNumber;
            }

            return Regex.Replace(phoneNumber, @"[^0-9+\- ()]", string.Empty);
        }

        public string SanitizeHtml(string inputString)
        {
             return _htmlSanitizer.Sanitize(inputString);
        }
    }
}
