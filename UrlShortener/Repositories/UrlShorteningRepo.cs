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

       
    }
}
