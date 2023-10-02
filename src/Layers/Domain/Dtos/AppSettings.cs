namespace Domain.Dtos
{
    public class AppSettings
    {
        public  string ShortenedBaseUrl { get; set; }
        public int MaxCodeLength { get; set; }
        public int UrlAllowedCharCount { get; set; }
        public string FirstKey { get; set;}
    }
}
