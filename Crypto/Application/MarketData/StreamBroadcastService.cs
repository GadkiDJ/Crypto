using Crypto.Application.Interfaces;
using Crypto.Application.MarketData.DTOs;
using Crypto.Application.MarketData.Enums;
using Microsoft.AspNetCore.SignalR;
using Crypto.Controllers.Hubs;
namespace Crypto.Application.MarketData
{
    public class StreamBroadcastService
    {
        public StreamBroadcastService(IMarketDataStreamService streamService, IHubContext<MarketDataHub> hubContext)
        {
            streamService.OnCandleReceived += async (symbol, interval, candle) =>
            {
                await hubContext.Clients.Group($"{symbol}_{interval}").SendAsync("ReceiveCandle", candle);
            };
        }
    }
}