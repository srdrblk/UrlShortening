namespace Domain.Entities
{
    public class OriginalUrl
    {
        public string? Url { get; set; }
        public bool IsCustom { get; set; }
    //    public string? Query { get; set; } // I don't have any direction about that 

        public OriginalUrl(string url)
        {
            Url =url;
        }
        public OriginalUrl(string url, bool isCustom = false) : this(url)
        {
            IsCustom = isCustom;
        }
        public void SetIsCustom(bool isCustom)
        {
            IsCustom= isCustom;
        }


    }
}
