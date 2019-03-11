using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CipherPark.TriggerOrange.Web.Models
{
    public class SendGridEventModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("smtp-id")]
        public string SmtpId { get; set; }

        [JsonProperty("useragent")]
        public string UserAgent { get; set; }

        [JsonProperty("IP")]
        public string IP { get; set; }

        [JsonProperty("sg_event_id")]
        public string EventId { get; set; }

        [JsonProperty("sg_message_id")]
        public string MessageId { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("response")]
        public string Response { get; set; }

        [JsonProperty("tls")]
        public string Tls { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("attempt")]
        public string Attempt { get; set; }

        [JsonProperty("category")]
        public string[] Categories { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("unique_args")]
        public JObject UniqueArgs { get; set; }
    }
}