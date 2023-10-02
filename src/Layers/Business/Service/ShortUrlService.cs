using Core.Context;
using Domain.Dtos;
using Domain.Dtos.ShortUrl;
using Domain.Entities;
using Domain.Extensions;
using Service.IService;
using Microsoft.Extensions.Options;
using Resources;

namespace Service.Service
{
    public class ShortUrlService : IShortUrlService
    {
        private UrlShorteningContext context;
        private AppSettings appSettings;
        public ShortUrlService( UrlShorteningContext _context, IOptions<AppSettings> _appSettings)
        {
            context = _context;
            appSettings = _appSettings.Value;
        }
        public BaseResponse<GetUrlDto> GetUrlWithShortenedUrl(string shortenedUrl)
        {
            var shortUrl = context.GetOriginalUrl(shortenedUrl);
            if (shortUrl != null)
            {
                return new BaseResponse<GetUrlDto>(new GetUrlDto(shortUrl.GetUrl()) );
            }
            return new BaseResponse<GetUrlDto>(MessagesResource.UrlUnassociated);
        }
        public BaseResponse<GetUrlDto> CreateShortUrl(CreateUrlDto createUrl)
        {
            if (!IsURLValid(createUrl.Url))
            {
                return new BaseResponse<GetUrlDto>(MessagesResource.UrlInvalid);
            }

            var url = new Uri(createUrl.Url);
            var absoluteUrl = url.AbsoluteUri.Replace(url.Query,"/");
            var originalUrl = new OriginalUrl(absoluteUrl);

            if (context.IsUrlExist(originalUrl.GetUrl()))
            {
                var shortenedUrl = context.GetShortenedKey(originalUrl.GetUrl());
                if (string.IsNullOrEmpty(shortenedUrl)) 
                {
                    return new BaseResponse<GetUrlDto>(MessagesResource.CodeNotExist);
                }
                return new BaseResponse<GetUrlDto>(new GetUrlDto(shortenedUrl) , MessagesResource.UrlExist);
            }

            if (string.IsNullOrEmpty(createUrl.CustomKey))
            {
                var key = context.GenerateKeyForUrl(originalUrl.GetUrl());
                if (key=="-1")
                {
                    return new BaseResponse<GetUrlDto>(MessagesResource.AllCombinationsUsed);
                }
                var shortenedUrl = context.AddShortUrl(key,originalUrl);
                return new BaseResponse<GetUrlDto>() { Data = new GetUrlDto(shortenedUrl) };
            }
            if(createUrl.CustomKey.Length>appSettings.MaxCodeLength)
            {
                return new BaseResponse<GetUrlDto>(string.Format( MessagesResource.CodeLengthExceed, appSettings.MaxCodeLength));
            }
            if (context.IsKeyExist( createUrl.CustomKey))
            {
                var shortenedUrl = context.GetOriginalUrl(createUrl.CustomKey);
                return new BaseResponse<GetUrlDto>(new GetUrlDto(shortenedUrl.GetUrl()),MessagesResource.CodeUsed);
            }
            else
            {
                originalUrl.SetIsCustom(true);
                var shortenedUrl = context.AddShortUrl(createUrl.CustomKey,originalUrl);
                return new BaseResponse<GetUrlDto>() { Data = new GetUrlDto(shortenedUrl)  };
            }
        }
        private bool IsURLValid(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }

    }
}
