using Core.Context;
using Domain.Dtos;
using Domain.Dtos.ShortUrl;
using Domain.Enums;
using Microsoft.Extensions.Options;
using Resources;
using Service.IService;
using Service.Service;
using static System.Net.WebRequestMethods;

namespace BusinessTest
{
    public class ShortUrlServiceTest : IDisposable
    {
        UrlShorteningContext context;
        IShortUrlService shortUrlService;
        IOptions<AppSettings> options;
        [OneTimeSetUp]
        public void Setup()
        {
            options = Options.Create<AppSettings>(new AppSettings() { FirstKey = "rrrrrr", MaxCodeLength = 6, ShortenedBaseUrl = "http://sample.site/", UrlAllowedCharCount = 62 });
            context = new UrlShorteningContext(options);
            shortUrlService = new ShortUrlService(context, options);

        }
        [Test, Order(1)]
        public void CreateShortUrl_ShouldLastIndexEquelToR_WhenAdd63ShortUrl()
        {
            var testUrl = "https://www.sample-site.com/karriere/berufserfahrene/direkteinstieg/x/";
            var createUrlDto = new CreateUrlDto() { Url = testUrl };
            for (int i = 0; i < 62; i++)
            {
                createUrlDto = new CreateUrlDto() { Url = testUrl.Replace("x", i.ToString()) };
                shortUrlService.CreateShortUrl(createUrlDto);
            }
            createUrlDto = new CreateUrlDto() { Url = testUrl.Replace("x", 62.ToString()) };
            var shortenedUrl = shortUrlService.CreateShortUrl(createUrlDto);
            Assert.That(shortenedUrl.StatusCode, Is.EqualTo(ResponseStatu.Success));
            Assert.That(shortenedUrl.Data.Url, Is.EqualTo(options.Value.ShortenedBaseUrl + "rrrrgr"));
        }
        [Test, Order(2)]
        public void CreateShortUrl_ShouldResultEquelToSendedKey_WhenAddCustomUrl()
        {
            var testUrl = "https://www.sample-site.com/karriere/berufserfahrene/direkteinstieg/64/";
            var createUrlDto = new CreateUrlDto() { Url = testUrl, CustomKey = "rrrrgN" };

            var shortenedUrl = shortUrlService.CreateShortUrl(createUrlDto);
            Assert.That(shortenedUrl.StatusCode, Is.EqualTo(ResponseStatu.Success));
            Assert.That(shortenedUrl.Data.Url, Is.EqualTo(options.Value.ShortenedBaseUrl + "rrrrgN"));
        }
        [Test, Order(3)]
        public void CreateShortUrl_ShouldPassTheCustomKey_WhenAdd2ShortUrlWithOutKey()
        {
            var testUrl = "https://www.sample-site.com/karriere/berufserfahrene/direkteinstieg/65/";
            var createUrlDto = new CreateUrlDto() { Url = testUrl };
            shortUrlService.CreateShortUrl(createUrlDto);

            createUrlDto = new CreateUrlDto() { Url = testUrl.Replace("65","66") };
            var shortenedUrl = shortUrlService.CreateShortUrl(createUrlDto);
            Assert.That(shortenedUrl.StatusCode, Is.EqualTo(ResponseStatu.Success));
            Assert.That(shortenedUrl.Data.Url, Is.EqualTo(options.Value.ShortenedBaseUrl + "rrrrgR"));
        }
        [Test, Order(4)]
        public void CreateShortUrl_ShouldReturnError_WhenAddInvalidCharToCustomKey()
        {
            var testUrl = "https://www.sample-site.com/karriere/berufserfahrene/direkteinstieg/67/";
            var createUrlDto = new CreateUrlDto() { Url = testUrl, CustomKey = "rrrrg." };

            var shortenedUrl = shortUrlService.CreateShortUrl(createUrlDto);
            Assert.That(shortenedUrl.StatusCode, Is.EqualTo(ResponseStatu.Error));
            Assert.That(shortenedUrl.Message, Is.EqualTo(MessagesResource.CodeInvalid));
        }
        [Test, Order(5)]
        public void CreateShortUrl_ShouldReturnWarning_WhenAddExistedCustomKey()
        {
            var testUrl = "https://www.sample-site.com/karriere/berufserfahrene/direkteinstieg/67/";
            var createUrlDto = new CreateUrlDto() { Url = testUrl, CustomKey = "rrrrrr" };

            var shortenedUrl = shortUrlService.CreateShortUrl(createUrlDto);
            Assert.That(shortenedUrl.StatusCode, Is.EqualTo(ResponseStatu.Warning));
            Assert.That(shortenedUrl.Message, Is.EqualTo(MessagesResource.CodeUsed));
        }
        [Test, Order(6)]
        public void CreateShortUrl_ShouldReturnError_WhenAddLongCustomKey()
        {
            var testUrl = "https://www.sample-site.com/karriere/berufserfahrene/direkteinstieg/67/";
            var createUrlDto = new CreateUrlDto() { Url = testUrl, CustomKey = "1234567" };

            var shortenedUrl = shortUrlService.CreateShortUrl(createUrlDto);
            Assert.That(shortenedUrl.StatusCode, Is.EqualTo(ResponseStatu.Error));
            Assert.That(shortenedUrl.Message, Is.EqualTo(string.Format(MessagesResource.CodeLengthExceed, options.Value.MaxCodeLength)));
        }
        public void Dispose()
        {

        }
    }
}
