using Crypto.Application.MarketData.DTOs;
using Crypto.Application.MarketData.Enums;

namespace Crypto.Application.Interfaces
{
    public interface IMarketDataStreamService
    {
        Task SubscribeAsync(string symbol, CandleInterval candleInterval);
        Task UnsubscribeAsync(string symblo, CandleInterval interval);
        event Action<string, CandleInterval, CandleDto>? OnCandleReceived;
    }
}
