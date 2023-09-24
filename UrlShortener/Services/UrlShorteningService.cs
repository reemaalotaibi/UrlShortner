namespace UrlShortener.Services
{
    public class UrlShorteningService
    {
        private readonly ApplicationDbContext _context;
       
        public UrlShorteningService(ApplicationDbContext context) 
        {
            _context = context;
        }
        public UrlShorteningService()
        {

        }

        
        public string SaveNewUrl(string longUrl, string code, HttpContext httpContext)
        {
            if (!ValidateCode(code))
            {
                return "700";
            }
            if (!ValidateLongUrl(longUrl)) //if false (error in url return that)
            {
                return "400";
            }

            ShortenedUrl shortenedUrl = new ShortenedUrl()
            {
                Id = Guid.NewGuid(),
                Code = code,
                LongUrl = longUrl,
                CreatedOnUtc = DateTime.UtcNow,
                ShortUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{code}"
            };
            _context.ShortenedUrls.Add(shortenedUrl);
            _context.SaveChanges();

            return shortenedUrl.ShortUrl;
          
        }

        private bool ExistEndPointInDb(string userCode)
        {
            var checkexistance = _context.ShortenedUrls.Where(s => s.Code == userCode || s.LongUrl == userCode).Select(c => c.ShortUrl).FirstOrDefault();
            if (checkexistance is not null)
            {
                return true;
            }
            return false;
        }

        private bool ValidateCode(string code)
        {
            if (code.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(code));
            }
            if (ExistEndPointInDb(code))
            {
                return false;
            }
            return true;
            
        }
        private bool ValidateLongUrl(string longUrl)
        {
            if (longUrl.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(longUrl));
            }
            else if (ExistEndPointInDb(longUrl))
            {
                throw new Exception($"{longUrl} already exists");
            }
            if (!Uri.TryCreate(longUrl, UriKind.Absolute, out _))
            {
                return false;
            }
            return true;
        }
     
        
       

    }
}
