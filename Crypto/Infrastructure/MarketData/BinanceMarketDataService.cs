using Crypto.Application.Interfaces;
using Crypto.Application.MarketData.DTOs;
using Crypto.Application.MarketData.Enums;
using Crypto.Application.MarketData.Mappers;
using Binance.Net.Clients;
using Binance.Net.Enums;


namespace Crypto.Infrastructure.MarketData
{
    public class BinanceMarketDataService : IMarketDataService
    {
        private readonly BinanceRestClient _client;
        public BinanceMarketDataService(BinanceRestClient client)
        {
            _client = client;
        }
        public async Task<IReadOnlyList<CandleDto>> GetCandlesAsync(
            string symbol,
            CandleInterval interval,
            int limit,
            CancellationToken cancellationToken = default
            )
        {
            var klineInterval = interval switch
            {
                CandleInterval.OneMinute => KlineInterval.OneMinute,
                CandleInterval.FiveMinutes => KlineInterval.FiveMinutes,
                CandleInterval.FifteenMinutes => KlineInterval.FifteenMinutes,
                CandleInterval.ThirtyMinutes => KlineInterval.ThirtyMinutes,
                CandleInterval.OneHour => KlineInterval.OneHour,
                CandleInterval.OneDay => KlineInterval.OneDay,
                _ => throw new ArgumentOutOfRangeException()
            };
            var result = await _client.SpotApi.ExchangeData.GetKlinesAsync(
                symbol,
                klineInterval,
                limit: limit,
                ct: cancellationToken
                );
            if(!result.Success)
                throw new Exception($"Binance API request failed: {result.Error}");

            return result.Data.Select(k =>  new CandleDto
            {
                Time =  new DateTimeOffset(k.OpenTime).ToUnixTimeMilliseconds(),
                Open = k.OpenPrice,
                Close = k.ClosePrice,
                High = k.HighPrice,
                Low = k.LowPrice,
                Volume = k.Volume,
            }).ToList();
        }
    }
}