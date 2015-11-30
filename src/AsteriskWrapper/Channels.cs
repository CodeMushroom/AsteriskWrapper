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
        public IAriClient AriClient { get; set; }

        public Channels(IAriClient ariClient)
        {
            AriClient = ariClient;
        }

        public ChannelWrapper this[string channelId]
        {
            get { return new ChannelWrapper(this, channelId); }
        }

        public Task AnswerAsync(string channelId)
        {
            return AnswerAsync(channelId, CancellationToken.None);
        }

        public async Task AnswerAsync(string channelId, CancellationToken cancellationToken)
        {
            var body = new StringContent("", Encoding.UTF8, "application/json");

            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.PostAsync($"/ari/channels/{channelId}/answer", body, cancellationToken))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public Task<IEnumerable<Channel>> GetActiveChannelsAsync()
        {
            return GetActiveChannelsAsync(CancellationToken.None);
        }

        public async Task<IEnumerable<Channel>> GetActiveChannelsAsync(CancellationToken cancellationToken)
        {
            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.GetAsync($"/ari/channels", cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<Channel[]>(await response.Content.ReadAsStringAsync());
            }
        }

        public Task<string> GetVariableAsync(string channelId, string variable)
        {
            return GetVariableAsync(channelId, variable, CancellationToken.None);
        }

        public async Task<string> GetVariableAsync(string channelId, string variable, CancellationToken cancellationToken)
        {
            var encodedVariable = Uri.EscapeDataString(variable);

            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.GetAsync($"/ari/channels/{channelId}/variable?variable={encodedVariable}", cancellationToken))
            {
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
            var content = JsonConvert.SerializeObject(new { variable, value });
            var body = new StringContent(content, Encoding.UTF8, "application/json");

            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.PostAsync($"/ari/channels/{channelId}/variable", body, cancellationToken))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public Task<Playback> StartPlaybackAsync(string channelId, string media)
        {
            return StartPlaybackAsync(channelId, media, CancellationToken.None);
        }

        public async Task<Playback> StartPlaybackAsync(string channelId, string media, CancellationToken cancellationToken)
        {
            var content = JsonConvert.SerializeObject(new { media });
            var body = new StringContent(content, Encoding.UTF8, "application/json");

            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.PostAsync($"/ari/channels/{channelId}/play", body, cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<Playback>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
