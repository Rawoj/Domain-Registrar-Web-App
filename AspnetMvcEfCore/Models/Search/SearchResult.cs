using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Models.Search
{
    public class SearchResult : ISearchResult
    {
        public string DomainName { get; set; }
        public string DomainAvailability { get; set; }

    }
}
