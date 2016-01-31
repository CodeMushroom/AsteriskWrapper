using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteriskWrapper
{
    public class OriginateParameters
    {
        public string Endpoint { get; set; }
        public string Extension { get; set; }
        public string Context { get; set; }
        public int Priority { get; set; }
        public CallerId CallerId { get; set; }
        public string Label { get; set; }
        public string App { get; set; }
        public string AppArgs { get; set; }
        private int timeout = 30;
        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
            }
        }
        public string ChannelId { get; set; }
        public string OtherChannelId { get; set; }
        public string Originator { get; set; }
    }
}
