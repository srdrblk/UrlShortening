namespace Domain.Dtos.ShortUrl
{
    public class GetUrlDto
    {
        public  string Url { get; set; }

        public GetUrlDto(string url)
        {
            Url = url;
        }
    }
}
