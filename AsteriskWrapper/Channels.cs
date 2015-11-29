using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AsteriskWrapper
{
    public class Channels : IChannels
    {
        public AriClient AriClient { get; set; }

        public Channels(AriClient ariClient)
        {
            AriClient = ariClient;
        }

        public Channel this[string channelId]
        {
            get { return new Channel(this, channelId); }
        }

        public Task AnswerAsync(string channelId)
        {
            return AnswerAsync(channelId, CancellationToken.None);
        }

        public async Task AnswerAsync(string channelId, CancellationToken cancellationToken)
        {
            using (var httpClient = AriClient.CreateHttpClient())
            {
                var response = await httpClient.PostAsync($"/ari/channels/{channelId}/answer", new StringContent(""), cancellationToken);
                response.EnsureSuccessStatusCode();
            }
        }

        public Task<string> GetVariableAsync(string channelId, string variable)
        {
            return GetVariableAsync(channelId, variable, CancellationToken.None);
        }

        public async Task<string> GetVariableAsync(string channelId, string variable, CancellationToken cancellationToken)
        {
            using (var httpClient = AriClient.CreateHttpClient())
            {
                var encodedVariable = Uri.EscapeDataString(variable);
                var response = await httpClient.GetAsync($"/ari/channels/{channelId}/variable?variable={encodedVariable}", cancellationToken);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }

        public Task SetVariableAsync(string channelId, string variable, string value)
        {
            return SetVariableAsync(channelId, variable, value, CancellationToken.None);
        }

        public async Task SetVariableAsync(string channelId, string variable, string value, CancellationToken cancellationToken)
        {
            using (var httpClient = AriClient.CreateHttpClient())
            {
                var content = JsonConvert.SerializeObject(new { variable, value });
                var body = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/ari/channels/{channelId}/variable", body, cancellationToken);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
