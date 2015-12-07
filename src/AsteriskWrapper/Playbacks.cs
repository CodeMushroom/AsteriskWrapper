using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AsteriskWrapper
{
    public class Playbacks : IPlaybacks
    {
        public IAriClient AriClient { get; set; }

        public Playbacks (IAriClient ariClient)
        {
            AriClient = ariClient;
        }

        public Task<Playback> GetDetailsAsync(string playbackId)
        {
            return GetDetailsAsync(playbackId, CancellationToken.None);
        }

        public async Task<Playback> GetDetailsAsync(string playbackId, CancellationToken cancellationToken)
        {
            using (var httpClient = AriClient.CreateHttpClient())
            using (var response = await httpClient.GetAsync($"/ari/playbacks/{playbackId}", cancellationToken))
            {
                if (!response.IsSuccessStatusCode)
                    throw await response.ToExceptionAsync();

                return JsonConvert.DeserializeObject<Playback>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
