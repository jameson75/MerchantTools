using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CipherPark.Ebay.Api
{
    public interface IEbayRequest
    {
        string OperationName { get; }
    }

    public enum ListingCodeType
    {
        AdType,
        Auction,
        Chinese,
        CustomCode,
        FixedPriceItem,
        Half,
        LeadGeneration,
        PersonalOffer,
        Shopping,
        Unknown,
        StoresFixedPrice
    }
}
