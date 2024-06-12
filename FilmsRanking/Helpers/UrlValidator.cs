namespace FilmsRanking.Helpers
{
    public static class UrlValidator
    {
        public static bool IsValidUrl(string url)
        {
            bool result = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && uriResult.Scheme == Uri.UriSchemeHttps;
            return result;
        }
    }
}
