using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Models.Purchase
{
    public class PurchaseDetailsModel
    {
        public string DomainName { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal UserBalance { get; set; }

    }
}
