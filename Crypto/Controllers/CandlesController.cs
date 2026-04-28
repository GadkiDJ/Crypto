using Crypto.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crypto.Application.MarketData.Enums;

namespace Crypto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CandlesController : ControllerBase
    {
        private readonly IMarketDataService _marketDataService;
        public CandlesController(IMarketDataService marketDataService)
        {
            _marketDataService = marketDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCandles(
            [FromQuery] string symbol,
            [FromQuery] CandleInterval interval,
            [FromQuery] int limit = 100,
            [FromQuery] CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                return BadRequest("Symbol is required.");

            var candles = await _marketDataService.GetCandlesAsync(symbol.ToUpper(), interval, limit, cancellationToken);
            return Ok(candles);
        }
    }
}
