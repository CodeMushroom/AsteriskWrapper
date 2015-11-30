using Newtonsoft.Json;

namespace AsteriskWrapper
{
    public class Dialplan
    {
        public string Context { get; set; }
        [JsonProperty("exten")]
        public string Extension { get; set; }
        public string Priority { get; set; }
    }
}