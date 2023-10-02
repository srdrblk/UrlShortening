using Domain.Dtos;
using Domain.Dtos.ShortUrl;
using Microsoft.AspNetCore.Mvc;
using Resources;
using Service.IService;
using System;

namespace UrlShortening.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrlService shortUrlService;

        public ShortUrlController(IShortUrlService _shortUrlService)
        {
            shortUrlService = _shortUrlService;
        }

        [HttpPost(Name = "CreateShortUrl")]
        public BaseResponse<GetUrlDto> CreateShortUrl([FromBody] CreateUrlDto createUrl)
        {
            return shortUrlService.CreateShortUrl(createUrl);
        }
        [HttpGet(Name = "GetUrl")]
        public BaseResponse<GetUrlDto> GetUrl(string shortenedUrl)
        {
            return shortUrlService.GetUrlWithShortenedUrl(shortenedUrl);
        }
 
    }
}