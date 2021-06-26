using DomainRegistrarWebApp.Models;
using DomainRegistrarWebApp.Models.Search;
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

        public SearchController(ILogger<SearchController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        private async Task<SearchResultViewModel> getSearchResult(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new System.ArgumentException($"'{nameof(query)}' cannot be null or empty.", nameof(query));
            }
            var key = _config.GetSection("WhoIsXMLAPI:ServiceApiKey").Value;
            Search s = new Search(key);
            var result = s.CheckAvailability(query);
            var r = await result;
            var searchResultView = new SearchResultViewModel
            {
                DomainAvailability = r.domainAvailability,
                DomainName = r.domainName
            };
            return searchResultView;
        }

        [HttpGet("query")]
        public async Task<IActionResult> SearchResult(string query)
        {
            return View(await getSearchResult(query));
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}