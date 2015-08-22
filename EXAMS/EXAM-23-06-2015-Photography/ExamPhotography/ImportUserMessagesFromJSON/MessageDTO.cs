using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportUserMessagesFromJSON
{
    public class MessageDTO
    {
        public string Content { get; set; }

        public DateTime DateTime { get; set; }

        public string Recipient { get; set; }

        public string Sender { get; set; }
    
    }
}
