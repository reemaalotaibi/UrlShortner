using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

namespace UrlShortener.Repositories
{
    public class UrlShorteningRepo
    {
        private IConfiguration _configuration;

        public UrlShorteningRepo(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public UrlShorteningRepo()
        {

        }

        public int GetUrlLen()
        {
            var dbConn = _configuration.GetSection("appsettings.json").GetSection("UrlOptions:UrlLength").Value;

            //var x = _configuration.GetSection("appsettings")["UrlOptions:UrlLength"];
            return int.Parse(dbConn);

        }
    }
}
