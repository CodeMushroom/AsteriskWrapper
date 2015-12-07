using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsteriskWrapper
{
    public interface IBridges
    {
        Task AddChannelAsync(string bridgeId, string channelId);
        Task AddChannelAsync(string bridgeId, string channelId, CancellationToken cancellationToken);
        Task<Bridge> CreateBridgeAsync();
        Task<Bridge> CreateBridgeAsync(CancellationToken cancellationToken);
        Task<Bridge> CreateBridgeAsync(string bridgeId);
        Task<Bridge> CreateBridgeAsync(string bridgeId, CancellationToken cancellationToken);
        Task<IEnumerable<Bridge>> GetActiveBridgesAsync();
        Task<IEnumerable<Bridge>> GetActiveBridgesAsync(CancellationToken cancellationToken);
        Task PlayMusicOnHoldAsync(string bridgeId);
        Task PlayMusicOnHoldAsync(string bridgeId, CancellationToken cancellationToken);
        Task StopMusicOnHoldAsync(string bridgeId);
        Task StopMusicOnHoldAsync(string bridgeId, CancellationToken cancellationToken);
    }
}
