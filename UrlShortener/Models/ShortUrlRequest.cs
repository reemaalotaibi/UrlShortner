namespace UrlShortener.Model
{
    public class ShortUrlRequest
    {
        public string URL { get; set; } = string.Empty; 
        public string Code { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
