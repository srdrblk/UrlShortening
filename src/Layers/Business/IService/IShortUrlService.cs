using Domain.Dtos.ShortUrl;
using Domain.Dtos;

namespace Service.IService
{
    public interface IShortUrlService
    {
        BaseResponse<GetUrlDto> GetUrlWithShortenedUrl(string shortenedUrl);
        BaseResponse<GetUrlDto> CreateShortUrl(CreateUrlDto createUrl);
    }
}
