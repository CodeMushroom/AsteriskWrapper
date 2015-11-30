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
        Task<Bridge> CreateBridgeAsync();
        Task<Bridge> CreateBridgeAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Bridge>> GetActiveBridgesAsync();
        Task<IEnumerable<Bridge>> GetActiveBridgesAsync(CancellationToken cancellationToken);
    }
}
