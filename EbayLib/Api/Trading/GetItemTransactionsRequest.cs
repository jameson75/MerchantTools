using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api.Trading
{
    [XmlRoot(Namespace = "urn:ebay:apis:eBLBaseComponents")]
    public class GetItemTransactionsRequest : IEbayRequest
    {
        public RequesterCredentials RequesterCredentials { get; set; }
        public string OperationName { get { return "GetItemTransactions"; } }
        public bool IncludeContainingOrder { get; set; }
        public bool IncludeFinalValueFee { get; set; }
        public bool IncludeVariations { get; set; }
        public string ItemID { get; set; }
        public string ModTimeFrom { get; set; }
        public string ModTimeTo { get; set; }
        public string NumberOfDays { get; set; }
        public string OrderLineItemID { get; set; }
        public Pagination Pagination { get; set; }
        //public TransactionPlatformCodeType Platform { get; set; }
        public string TransactionID { get; set; }
        public string DetailLevel { get; set; }
    }

    public enum TransactionPlatformCodeType
    {
        CustomCode,
        eBay,
        Express,
        Half,
        Shopping,
        WorldOfGood
    }
}
