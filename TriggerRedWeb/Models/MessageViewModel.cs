using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CipherPark.TriggerOrange.Web.Models
{
    public class MessageViewModel
    {
        public string IconName { get; set; }
        public string MessageType { get; set; }
        public string Text { get; set; }
        public long Id { get; set; }
    }
}