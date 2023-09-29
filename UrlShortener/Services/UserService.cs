namespace UrlShortener.API.Services
{
    public class UserService
    {
        public UserService() { }

        public bool ValidateToken(string token)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }
            return true;
        }
      
    }
}
