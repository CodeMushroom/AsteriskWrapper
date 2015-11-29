using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsteriskWrapper
{
    public interface IChannels
    {
        Task AnswerAsync(string channelId);
        Task AnswerAsync(string channelId, CancellationToken cancellationToken);
        Task<string> GetVariableAsync(string channelId, string variable);
        Task<string> GetVariableAsync(string channelId, string variable, CancellationToken cancellationToken);
        Task SetVariableAsync(string channelId, string variable, string value);
        Task SetVariableAsync(string channelId, string variable, string value, CancellationToken cancellationToken);
    }
}
