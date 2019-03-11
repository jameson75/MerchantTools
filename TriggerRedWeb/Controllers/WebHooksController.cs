using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CipherPark.TriggerOrange.Web.Models;

namespace CipherPark.TriggerOrange.Web.Controllers
{
    public class WebHooksController : ApiController
    {
        [HttpPost]
        public void SendGridEvent([FromBody]SendGridEventModel[] requestContent)
        {
            //TODO: Respond to subscribed user events.                        
        }
    }
}
