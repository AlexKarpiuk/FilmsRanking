namespace FilmsRanking.Interfaces
{
    public interface ISanitizerService
    {
        string SanitizePhoneNumber(string phoneNumber);
        string SanitizeHtml(string inputString);
    }
}
