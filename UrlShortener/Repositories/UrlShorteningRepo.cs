namespace UrlShortener.Repositories
{
    public class UrlShorteningRepo
    {
        private readonly IConfiguration _configuration;

        public UrlShorteningRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       

        public int GetUrlLen()
        {
            var UrlLength = _configuration.GetValue<string>("UrlOptions:UrlLength");
            if (UrlLength == null)
            {
                return 8;
            }
            return int.Parse(UrlLength);
        }
    }
}
