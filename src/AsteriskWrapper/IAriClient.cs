using System.Net.Http;

namespace AsteriskWrapper
{
    public interface IAriClient
    {
        HttpClient CreateHttpClient();
        IChannels Channels { get; }
        IBridges Bridges { get; }
    }
}