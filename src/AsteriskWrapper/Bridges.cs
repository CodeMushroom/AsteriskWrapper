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
    public class Bridges : IBridges
    {
        public IAriClient AriClient { get; set; }

        public Bridges(IAriClient ariClient)
        {
            AriClient = ariClient;
        }

        public Task AddChannelAsync(string bridgeId, string channelId)
        {
            return AddChannelAsync(bridgeId, channelId, CancellationToken.None);
        }

        public async Task AddChannelAsync(string bridgeId, string channelId, CancellationToken cancellationToken)
        {
            var body = new StringContent(JsonConvert.SerializeObject(new { channel = channelId }), Encoding.UTF8, "application/json");

            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.PostAsync($"/ari/bridges/{bridgeId}/addChannel", body, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);
            }
        }

        public Task<Bridge> CreateBridgeAsync()
        {
            return CreateBridgeAsync(CancellationToken.None);
        }

        public Task<Bridge> CreateBridgeAsync(string bridgeId)
        {
            return CreateBridgeAsync(bridgeId, CancellationToken.None);
        }

        public Task<Bridge> CreateBridgeAsync(CancellationToken cancellationToken)
        {
            return CreateBridgeAsync(null, cancellationToken);
        }

        public async Task<Bridge> CreateBridgeAsync(string bridgeId, CancellationToken cancellationToken)
        {
            var body = new StringContent("", Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(bridgeId))
                bridgeId = "/" + bridgeId;
            else
                bridgeId = "";

            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.PostAsync($"/ari/bridges{bridgeId}", body, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<Bridge>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
        }

        public Task<IEnumerable<Bridge>> GetActiveBridgesAsync()
        {
            return GetActiveBridgesAsync(CancellationToken.None);
        }

        public async Task<IEnumerable<Bridge>> GetActiveBridgesAsync(CancellationToken cancellationToken)
        {
            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.GetAsync($"/ari/bridges", cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<Bridge[]>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
        }

        public Task PlayMusicOnHoldAsync(string bridgeId)
        {
            return PlayMusicOnHoldAsync(bridgeId, CancellationToken.None);
        }

        public async Task PlayMusicOnHoldAsync(string bridgeId, CancellationToken cancellationToken)
        {
            var body = new StringContent("", Encoding.UTF8, "application/json");

            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.PostAsync($"/ari/bridges/{bridgeId}/moh", body, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);
            }
        }

        public Task StopMusicOnHoldAsync(string bridgeId)
        {
            return StopMusicOnHoldAsync(bridgeId, CancellationToken.None);
        }

        public async Task StopMusicOnHoldAsync(string bridgeId, CancellationToken cancellationToken)
        {
            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.DeleteAsync($"/ari/bridges/{bridgeId}/moh", cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync().ConfigureAwait(false);
            }
        }
    }
}
