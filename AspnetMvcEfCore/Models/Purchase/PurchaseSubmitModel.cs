using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Models.Purchase
{
    public class PurchaseSubmitModel
    {
        public string DomainName { get; set; }
        public decimal FullPrice { get; set; }

        /// <summary>
        /// Amount of discount in percents.
        /// </summary>
        public decimal PromoAmount { get; set; }
        
        // CC details omitted on purpouse
        
    }
}
