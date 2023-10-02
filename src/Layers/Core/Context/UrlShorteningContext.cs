using Domain.Dtos;
using Domain.Entities;
using Domain.Extensions;
using Microsoft.Extensions.Options;
using System.Text;

namespace Core.Context
{
    public class UrlShorteningContext
    {
        private readonly char[] RandomlySortedUrlCharacters = new char[] { 'r', 'g', 'N', 'R', 'F', 'K', 'p', 'P', 'I', 'v', 'X', 'u', 'h', 'b', 's', 'T', 'J', 'j', 'm', 'Q', 'E', '6', 'W', 'l', 'V', 'o', 'O', 'f', '5', 'c', 'D', 't', 'U', 'Y', '7', 'd', '1', 'Z', 'A', '0', 'M', '9', 'H', '3', 'y', 'i', '4', 'w', 'n', '2', 'z', 'G', 'e', 'C', 'x', 'a', 'S', '8', 'q', 'L', 'B', 'k' };
        private int LastIndex { get; set; }
        private Dictionary<string, OriginalUrl> UrlCollection = new Dictionary<string, OriginalUrl>(); //use key of dictionary like indexed id
        private readonly string FirstKey ;// "rrrrr" is our start point like "000000"
        private readonly int StartIndex ;
        private AppSettings appSettings;
        public UrlShorteningContext(IOptions<AppSettings> _options)
        {
            appSettings = _options.Value;
            LastIndex = appSettings.UrlAllowedCharCount - 1;
            StartIndex = appSettings.MaxCodeLength - 1;
            FirstKey = appSettings.FirstKey;
        }

        public OriginalUrl GetOriginalUrl(string key)
        {
            key = key.Replace(appSettings.ShortenedBaseUrl, "");
            return UrlCollection[key];
        }
        public string GetShortenedKey(string url)
        {
            var record = UrlCollection.FirstOrDefault(uc => uc.Value.GetUrl() == url);
            if (record.Key != null)
            {
                var key = UrlCollection.FirstOrDefault(uc => uc.Value.GetUrl() == url).Key;
                return appSettings.ShortenedBaseUrl + key;
            }
            return string.Empty;
        }
        public string AddShortUrl(string key, OriginalUrl shortUrl)
        {
            UrlCollection.Add(key, shortUrl);
            return appSettings.ShortenedBaseUrl + key;
        }

        public string GenerateKeyForUrl(string originalUrl)
        {
            var urlCollection = UrlCollection.LastOrDefault(uc => !uc.Value.IsCustom); //We take the last non-custom code of this host address, because if we try to generate the next key code from the custom code, we will create a space between the codes.
            if (urlCollection.Key == null)
            {
                if (!IsKeyExist(FirstKey))
                {
                    return FirstKey;  // return "rrrrr" 
                }
               return GenerateNextKey(FirstKey);
            }
            return GenerateNextKey( urlCollection.Key);
        }
        private string GenerateNextKey( string key)
        {
            return CheckKey(key, StartIndex);
        }
        /* Check Key : 
        If the last key code for the sent host address is "rrrrrr", we increase the character by one, starting from the end.
        We do the incrementing process according to the order found in the RandomlySortedUrlCharacters array.
        In this case, the newly generated key code is "rrrrrrg".
        If the last used key code had been "abcdk", the newly generated code would have been "abcd1k" since k is the last character in the RandomlySortedUrlCharacters.
        After generating the key code with the recursion method, it is checked again in case this code is custom or created in another way,
        and the key code generation step is called again depending on the situation.
         */
        private string CheckKey(string key, int index)
        {
            if (index<0)
            {
                return "-1";
            }
            StringBuilder newKey = new StringBuilder(key);
            var keyIndex = Array.IndexOf(RandomlySortedUrlCharacters, key[index]);
            if (keyIndex < LastIndex) // if it is not last char on RandomlySortedUrlCharacters
            {
                newKey[index] = RandomlySortedUrlCharacters[keyIndex + 1]; // then replace it with the next character in RandomlySortedUrlCharacters
            }
            else
            {
                newKey[index] = RandomlySortedUrlCharacters[0]; //reset char with "r" ; "r" is our first char in RandomlySortedUrlCharacters
                newKey = new StringBuilder(CheckKey(newKey.ToString(), index - 1));
            }
            if (IsKeyExist(newKey.ToString()))
            {
                return CheckKey(newKey.ToString(), StartIndex);
            }
            return newKey.ToString();
        }
        public bool IsKeyExist(string key)
        {
            return UrlCollection.ContainsKey(key);
        }
        public bool IsKeyValid(string key)
        {
            foreach (var ch in key)
            {
                if (!RandomlySortedUrlCharacters.Contains(ch))
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsUrlExist(string url)
        {
            return UrlCollection.Any(uc => uc.Value.GetUrl() == url);
        }
    }
}
