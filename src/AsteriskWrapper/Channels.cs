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
            using (var response = await httpClient.PostAsync($"/ari/channels/{channelId}/answer", body, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);
            }
        }

        public Task<IEnumerable<Channel>> GetActiveChannelsAsync()
        {
            return GetActiveChannelsAsync(CancellationToken.None);
        }

        public async Task<IEnumerable<Channel>> GetActiveChannelsAsync(CancellationToken cancellationToken)
        {
            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.GetAsync($"/ari/channels", cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<Channel[]>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
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
            using (var response = await httpClient.GetAsync($"/ari/channels/{channelId}/variable?variable={encodedVariable}", cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<Variable>(await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Value;
            }
        }

        public Task HangupAsync(string channelId)
        {
            return HangupAsync(channelId, CancellationToken.None);
        }

        public async Task HangupAsync(string channelId, CancellationToken cancellationToken)
        {
            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.DeleteAsync($"/ari/channels/{channelId}", cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);
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
            using (var response = await httpClient.PostAsync($"/ari/channels/{channelId}/variable", body, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);
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
            using (var response = await httpClient.PostAsync($"/ari/channels/{channelId}/play", body, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<Playback>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
        }

        public Task<Channel> GetChannelAsync(string channelId)
        {
            return GetChannelAsync(channelId, CancellationToken.None);
        }

        public async Task<Channel> GetChannelAsync(string channelId, CancellationToken cancellationToken)
        {
            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.GetAsync($"/ari/channels/{channelId}", cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<Channel>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
        }

        public Task<string> CreateChannel(string endpoint, string extension, string context, string priority, string app, CallerId callerId, string label, string appArgs, int timeout, string channelId, string otherChannelId, string originator)
        {
            return CreateChannel(endpoint, extension, context, priority, app, callerId, label, appArgs, timeout, channelId, otherChannelId, originator, CancellationToken.None);
        }

        public async Task<string> CreateChannel(string endpoint, string extension, string context, string priority, string app, CallerId callerId, string label, string appArgs, int timeout, string channelId, string otherChannelId, string originator, CancellationToken cancellationToken)
        {
            var content = JsonConvert.SerializeObject(new
            {
                endpoint = endpoint,
                extension = extension,
                context = context,
                priority = priority,
                app = app,
                callerid = $"{callerId.Name}<{callerId.Number}>",
                label = label,
                appArgs = appArgs,
                timeout = timeout,
                channelId = channelId,
                otherChannelId = otherChannelId
            });
            var body = new StringContent(content, Encoding.UTF8, "application/json");

            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.PostAsync($"/ari/channels", body, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);
                return response.ToString();
            }
        }
    }
}
