using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spp3.Data;
using spp3.Data.Models;

namespace spp3.Controllers
{
    [ApiController]
    [Route("deals")]
    public class DealsController : Controller
    {
        private readonly ILogger<DealsController> _logger;
        private readonly ShopContext _shopContext;
        public DealsController(ILogger<DealsController> logger, ShopContext context)
        {
            _shopContext = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var deals = _shopContext.Deals.Include(dl => dl.Seller).ThenInclude(sel => sel.OutletSection).ThenInclude(os => os.TradeOutlet).Include(dl => dl.Product).Include(dl => dl.Customer).ToList();
            return Ok(deals);
        }

        [HttpGet("{seller}")]
        public async Task<IActionResult> GetBySeller(Seller seller)
        {
            var deal = _shopContext.Deals.Include(dl => dl.Seller).Include(dl => dl.Product).Include(dl => dl.Customer).FirstOrDefault(dl => dl.Seller == seller);
            return Ok(deal);
        }

        [HttpGet("{product}")]
        public async Task<IActionResult> GetByProduct(Product product)
        {
            var deal = _shopContext.Deals.Include(dl => dl.Seller).Include(dl => dl.Product).Include(dl => dl.Customer).FirstOrDefault(dl => dl.Product == product);
            return Ok(deal);
        }

        [HttpGet("{customer}")]
        public async Task<IActionResult> GetByCustomer(Customer customer)
        {
            var deal = _shopContext.Deals.Include(dl => dl.Seller).Include(dl => dl.Product).Include(dl => dl.Customer).FirstOrDefault(dl => dl.Customer == customer);
            return Ok(deal);
        }

        [HttpGet("{dlId}")]
        public async Task<IActionResult> GetByCustomer(int dlId)
        {
            var deal = _shopContext.Deals.Include(dl => dl.Seller).Include(dl => dl.Product).Include(dl => dl.Customer).FirstOrDefault(dl => dl.dlId == dlId);
            return Ok(deal);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Deal deal)
        {
            _shopContext.Deals.Add(deal);
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{dlId}")]
        public async Task<IActionResult> Put(int dlId, Deal _deal)
        {
            var deal = _shopContext.Deals.FirstOrDefault(dl => dl.dlId == dlId);
            deal.prId = _deal.prId;
            deal.cuId = _deal.cuId;
            deal.selId = _deal.selId;
            deal.dealMoment = _deal.dealMoment;
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{dlId}")]
        public async Task<IActionResult> delete(int dlId)
        {
            var deletedDeal = _shopContext.Deals.FirstOrDefault(dl => dl.dlId == dl.dlId);
            _shopContext.Deals.Remove(deletedDeal);
            _shopContext.SaveChanges();
            return Ok();
        }
    }
}
