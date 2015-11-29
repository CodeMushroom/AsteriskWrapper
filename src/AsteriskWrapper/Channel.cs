using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsteriskWrapper
{
    public class Channel
    {
        public virtual string ChannelId { get; set; }
        public virtual IChannels Channels { get; set; }

        public Channel(IChannels channels, string channelId)
        {
            Channels = channels;
            ChannelId = channelId;
        }

        public Task AnswerAsync()
        {
            return Channels.AnswerAsync(ChannelId);
        }

        public Task AnswerAsync(CancellationToken cancellationToken)
        {
            return Channels.AnswerAsync(ChannelId, cancellationToken);
        }

        public Task<string> GetVariableAsync(string variable)
        {
            return Channels.GetVariableAsync(ChannelId, variable);
        }

        public Task<string> GetVariableAsync(string variable, CancellationToken cancellationToken)
        {
            return Channels.GetVariableAsync(ChannelId, variable, cancellationToken);
        }

        public Task SetVariableAsync(string variable, string value)
        {
            return Channels.SetVariableAsync(ChannelId, variable, value);
        }

        public Task SetVariableAsync(string variable, string value, CancellationToken cancellationToken)
        {
            return Channels.SetVariableAsync(ChannelId, variable, value, cancellationToken);
        }
    }
}
