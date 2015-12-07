using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsteriskWrapper
{
    public interface IPlaybacks
    {
        Task<Playback> GetDetailsAsync(string playbackId);
        Task<Playback> GetDetailsAsync(string playbackId, CancellationToken cancellationToken);
    }
}
