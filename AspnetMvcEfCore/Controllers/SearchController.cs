using DomainRegistrarWebApp.Database;
using DomainRegistrarWebApp.Models;
using DomainRegistrarWebApp.Models.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SearchController> _logger;
        private readonly ApplicationDbContext _db;
        public SearchController(ILogger<SearchController> logger, IConfiguration config, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Index(string q)
        {
            var url = Url.Action("SearchResult", "Search", new { query = q });
            return Redirect(url);
        }

        public async Task<SearchResultViewModel> GetSearchResult(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new System.ArgumentException($"'{nameof(query)}' cannot be null or empty.", nameof(query));
            }
            var key = _config.GetSection("WhoIsXMLAPI:ServiceApiKey").Value;
            ISearch s = new Search(key, _db);
            var result = await s.CheckAvailability(query);
            var searchResultView = new SearchResultViewModel
            {
                DomainAvailability = result.DomainAvailability,
                DomainName = result.DomainName
            };
            return searchResultView;
        }

        [HttpGet("query")]
        [Authorize]
        public async Task<IActionResult> SearchResult(string query)
        {
            return View(await GetSearchResult(query));
        }
        
        /*
        [HttpPost]
        [Authorize]
        public IActionResult SearchResult()
        {
            var url = Url.Action("Index", "Purchase");
            return Redirect(url);
        }
        */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}