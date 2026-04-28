using Crypto.Application.Interfaces;
using Crypto.Application.MarketData.DTOs;
using Crypto.Application.MarketData.Enums;
using Binance.Net.Clients;
using Binance.Net.Enums;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Objects.Sockets;

namespace Crypto.Application.MarketData
{
    public class BinanceStreamService : IMarketDataStreamService

    {
        private readonly BinanceSocketClient _socketClient;
        private readonly Dictionary<string, UpdateSubscription> _subscribtions = new();
        public event Action<string, CandleInterval, CandleDto>? OnCandleReceived;

        public BinanceStreamService(BinanceSocketClient socketClient)
        {
            _socketClient = socketClient;
        }

        public async Task SubscribeAsync(string symbol, CandleInterval interval)
        {
            var key = $"{symbol}_{interval}";

            if(_subscribtions.ContainsKey(key))
                return;

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

            var result = await _socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(symbol, klineInterval, data =>
            {
                var k = data.Data.Data;

                var candle = new CandleDto
                {
                    Time = new DateTimeOffset(k.OpenTime).ToUnixTimeMilliseconds(),
                    Open = k.OpenPrice,
                    Close = k.ClosePrice,
                    High = k.HighPrice,
                    Low = k.LowPrice,
                    Volume = k.Volume
                };
                OnCandleReceived?.Invoke(symbol, interval, candle);
            });
            if (!result.Success)
                throw new Exception(result.Error?.Message);

            _subscribtions[key] = result.Data;
        }

        public async Task UnsubscribeAsync(string symbol, CandleInterval interval)
        {
            var key = $"{symbol}_{interval}";
            if(!_subscribtions.TryGetValue(key, out var sub))
               return;
            
            await _socketClient.UnsubscribeAsync(sub);
            _subscribtions.Remove(key);
        }
    }
}
