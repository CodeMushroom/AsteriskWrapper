using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsteriskWrapper
{
    public class ChannelWrapper
    {
        public virtual string ChannelId { get; set; }
        public virtual IChannels Channels { get; set; }

        public ChannelWrapper(IChannels channels, string channelId)
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

        public Task HangupAsync()
        {
            return Channels.HangupAsync(ChannelId);
        }

        public Task HangupAsync(CancellationToken cancellationToken)
        {
            return Channels.HangupAsync(ChannelId, cancellationToken);
        }

        public Task SetVariableAsync(string variable, string value)
        {
            return Channels.SetVariableAsync(ChannelId, variable, value);
        }

        public Task SetVariableAsync(string variable, string value, CancellationToken cancellationToken)
        {
            return Channels.SetVariableAsync(ChannelId, variable, value, cancellationToken);
        }

        public Task<Playback> StartPlaybackAsync(string media)
        {
            return Channels.StartPlaybackAsync(ChannelId, media);
        }

        public Task<Playback> StartPlaybackAsync(string media, CancellationToken cancellationToken)
        {
            return Channels.StartPlaybackAsync(ChannelId, media, cancellationToken);
        }

        public Task<Channel> GetChannel()
        {
            return Channels.GetChannelAsync(ChannelId);
        }
    }
}
