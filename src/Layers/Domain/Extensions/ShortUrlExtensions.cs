using Domain.Entities;

namespace Domain.Extensions
{
    public static class ShortUrlExtensions
    {
        public static string GetUrl(this OriginalUrl shortUrl)
        {
            return shortUrl.Url; // To avoid updating all usage locations in case of any changes. Add or remove query or something else
        }
      
    }
}
