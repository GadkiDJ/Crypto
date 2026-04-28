using Crypto.Application.MarketData.Enums;

namespace Crypto.Application.MarketData.Mappers
{
    public static class IntervalMapper
    {
        public static string ToBinance(this CandleInterval interval)
        {
            return interval switch
            {
                CandleInterval.OneMinute => "1m",
                CandleInterval.FiveMinutes => "5m",
                CandleInterval.FifteenMinutes => "15m",
                CandleInterval.ThirtyMinutes => "30m",
                CandleInterval.OneHour => "1h",
                CandleInterval.OneDay => "1d",
                _ => throw new ArgumentOutOfRangeException(nameof(interval), interval, null)
            };
        }
    }
}
