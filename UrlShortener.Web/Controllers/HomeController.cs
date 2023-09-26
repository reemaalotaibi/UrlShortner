using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using UrlShortener.Web.Models;
namespace UrlShortener.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetShortUrl(InputUrl inputUrl)
        {
            var apiBaseUrl = _configuration.GetSection("BaseUrl").Value + "api/URlshortener";

            string url = "";
            
            if (inputUrl !=null)
            {
                using(var client = new HttpClient())
                {
                    var res = client.PostAsync(apiBaseUrl,
                        new StringContent(JsonConvert.SerializeObject(
                            inputUrl), Encoding.UTF8, "application/json"));

                    try
                    {
                        res.Result.EnsureSuccessStatusCode();
                        return View(res.Result);
                        
                    }
                    catch
                    {
                        res.Result.StatusCode.ToString();
                        return View(res.Result);
                        
                    }
                }
            }
            return View();

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}