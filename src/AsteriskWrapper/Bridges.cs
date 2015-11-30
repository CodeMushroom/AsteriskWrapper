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

        public Task<Bridge> CreateBridgeAsync()
        {
            return CreateBridgeAsync(CancellationToken.None);
        }

        public async Task<Bridge> CreateBridgeAsync(CancellationToken cancellationToken)
        {
            var body = new StringContent("", Encoding.UTF8, "application/json");

            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.PostAsync($"/ari/bridges", body, cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<Bridge>(await response.Content.ReadAsStringAsync());
            }
        }

        public Task<IEnumerable<Bridge>> GetActiveBridgesAsync()
        {
            return GetActiveBridgesAsync(CancellationToken.None);
        }

        public async Task<IEnumerable<Bridge>> GetActiveBridgesAsync(CancellationToken cancellationToken)
        {
            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.GetAsync($"/ari/bridges", cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<Bridge[]>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
