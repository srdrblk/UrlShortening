namespace Domain.Dtos.ShortUrl
{
    public class CreateUrlDto
    {
        public required string Url { get; set; }
        public string? CustomKey { get; set; }
    }
}
