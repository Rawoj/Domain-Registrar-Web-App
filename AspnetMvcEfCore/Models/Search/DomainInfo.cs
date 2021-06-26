using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Models.Search
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class DomainInfo
    {
        public string domainAvailability { get; set; }
        public string domainName { get; set; }
    }

    public class Root
    {
        public DomainInfo DomainInfo { get; set; }
    }
}
