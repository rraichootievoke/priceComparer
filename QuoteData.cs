using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCompareApp
{
    public class QuoteData
    {
        public string MyDoorOrderNum { get; set; }
        public string? DealerAccountNum { get; set; }
        public string? QuoteName { get; set; }
        public string? QuoteNumber { get; set; }
        public string? LineNo { get; set; }
        public string? LineTotalPrice { get; set; }
        public string? LineItemPriceDescription { get; set; }
        public string? LineItemConfigDescription { get; set; }
        public string? QuoteTotalPrice { get; set; }
        public string? IsConfigurationChanged { get; set; }
        public string? IsLinesAddedDeleted { get; set; }
    }

}
