using Newtonsoft.Json;

namespace AsteriskWrapper
{
    public class Playback
    {
        public string Id { get; set; }
        public string Language { get; set; }
        [JsonProperty("media_uri")]
        public string MediaUri { get; set; }
        public string State { get; set; }
        [JsonProperty("target_uri")]
        public string TargetUri { get; set; }
    }
}