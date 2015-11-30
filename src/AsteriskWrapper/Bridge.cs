using System.Collections.Generic;
using Newtonsoft.Json;

namespace AsteriskWrapper
{
    public class Bridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Technology { get; set; }
        public string Creator { get; set; }
        public IEnumerable<string> Channels { get; set; }
        [JsonProperty("bridge_type")]
        public string BridgeType { get; set; }
        [JsonProperty("bridge_class")]
        public string BridgeClass { get; set; }
    }
}