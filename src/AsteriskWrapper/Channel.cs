using System;

namespace AsteriskWrapper
{
    public class Channel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public CallerId Caller { get; set; }
        public CallerId Connected { get; set; }
        public string AccountCode { get; set; }
        public Dialplan DialPlan { get; set; }
        public DateTime CreationTime { get; set; }
        public string Lanugage { get; set; }
    }
}