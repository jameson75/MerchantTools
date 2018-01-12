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
    public class GetCategoriesRequest : IEbayRequest
    {        
        public GetCategoriesRequest()
        {
            ViewAllNodes = true;
        }

        public RequesterCredentials RequesterCredentials { get; set; }
        [XmlElement("CategoryParent")]
        public string[] CategoryParents { get; set; }
        public string CategorySiteID { get; set; }
        public int LevelLimit { get; set; }
        public bool ViewAllNodes { get; set; }       
        [XmlElement("DetailLevel")] 
        public string[] DetailLevel { get; set; }
        public string ErrorMessage { get; set; }
        public string MessageID { get; set; }
        [XmlElement("OutputSelector")]
        public string[] OutputSelector { get; set; }
        public string Version { get; set; }
        public string WarningLevel { get; set; }
        public string OperationName { get { return "GetCategories"; } }
    }       

    public class RequesterCredentials
    {
        [XmlElement("eBayAuthToken")]
        public string EBayAuthToken { get; set; }
    }
}
