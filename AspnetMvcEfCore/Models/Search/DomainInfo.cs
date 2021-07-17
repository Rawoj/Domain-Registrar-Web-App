using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Models.Search
{
    public class DomainInfo
    {
        public string DomainAvailability { get; set; }
        public string DomainName { get; set; }
    }

    public class Root
    {
        public DomainInfo DomainInfo { get; set; }
    }
}
