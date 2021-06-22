using DomainRegistrarWebApp.Models;
using DomainRegistrarWebApp.Models.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DomainRegistrarWebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        public IActionResult SearchResults(string q)
        {
           
            if (string.IsNullOrEmpty(q))
            {
                throw new System.ArgumentException($"'{nameof(q)}' cannot be null or empty.", nameof(q));
            }

            ISearch s = new Search();
            var result = s.CheckAvailability(q);
            var searchResultView = new SearchResultViewModel
            {
                DomainAvailability = result.DomainAvailability,
                DomainName = result.DomainName
            };
            return View(searchResultView);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}