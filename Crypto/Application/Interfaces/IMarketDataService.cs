using Crypto.Application.MarketData.DTOs;
using Crypto.Application.MarketData.Enums;

namespace Crypto.Application.Interfaces
{
    public interface IMarketDataService
    {
        Task<IReadOnlyList<CandleDto>> GetCandlesAsync(
            string symbol,
            CandleInterval interval,
            int limit,
            CancellationToken cancellationToken = default
            );
    }
}
