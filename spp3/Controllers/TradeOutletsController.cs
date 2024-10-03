using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spp3.Data;

namespace spp3.Controllers
{
    [ApiController]
    [Route("tradeoutlets")]
    public class TradeOutletsController : Controller
    {
        private readonly ILogger<TradeOutletsController> _logger;
        private readonly ShopContext _shopContext;
        public TradeOutletsController(ILogger<TradeOutletsController> logger, ShopContext context)
        {
            _shopContext = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tradeOulet = _shopContext.TradeOutlets.Include(to => to.OutletSections).Include(to => to.CommercialOrganization).ToList();
            return Ok(tradeOulet);
        }

        [HttpGet("{toId}")]
        public async Task<IActionResult> GetByName(int toId)
        {
            var comOrg = _shopContext.TradeOutlets.Include(to => to.OutletSections).FirstOrDefault(to => to.toId == toId);
            return Ok(comOrg);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TradeOutlet tradeOutlet)
        {
            _shopContext.TradeOutlets.Add(tradeOutlet);
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{toId}")]
        public async Task<IActionResult> Put(int toId, TradeOutlet _tradeOutlet)
        {

            var tradeOutlet = _shopContext.TradeOutlets.FirstOrDefault(to => to.toId == toId);

            if (tradeOutlet == null)
            {
                return NotFound();
            }
            tradeOutlet.outletName = _tradeOutlet.outletName;
            tradeOutlet.outletType = _tradeOutlet.outletType;
            tradeOutlet.rent = _tradeOutlet.rent;
            tradeOutlet.counters = _tradeOutlet.counters;
            tradeOutlet.size = _tradeOutlet.size;
            tradeOutlet.coId = _tradeOutlet.coId;
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{toId}")]
        public async Task<IActionResult> Delete(int toId)
        {
            var deletedTradeOutlet = _shopContext.TradeOutlets.FirstOrDefault(to => to.toId == toId);
            _shopContext.TradeOutlets.Remove(deletedTradeOutlet);
            _shopContext.SaveChanges();
            return Ok();
        }
    }
}
