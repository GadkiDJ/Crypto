using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Crypto.Application.Interfaces;
using Crypto.Application.MarketData.Enums;
namespace Crypto.Controllers.Hubs
{
    [Authorize]
    public class MarketDataHub : Hub
    {
        private readonly IMarketDataStreamService _streamService;

        public MarketDataHub(IMarketDataStreamService streamService)
        {
            _streamService = streamService;
        }

        public async Task Subscribe(string symbol, CandleInterval interval)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                $"{symbol}_{interval}");
            await _streamService.SubscribeAsync(symbol, interval);
        }
        public async Task Unsubscribe(string symbol, CandleInterval interval)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
                $"{symbol}_{interval}");
            await _streamService.UnsubscribeAsync(symbol, interval);
        }
    }
}