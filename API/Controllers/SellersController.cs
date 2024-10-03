using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spp3.Data;

namespace spp3.Controllers
{
    [ApiController]
    [Route("sellers")]
    public class SellersController : Controller
    {
        private readonly ILogger<SellersController> _logger;
        private readonly ShopContext _shopContext;
        public SellersController(ILogger<SellersController> logger, ShopContext context)
        {
            _shopContext = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sellers = _shopContext.Sellers.Include(sel => sel.OutletSection).ThenInclude(outlet => outlet.TradeOutlet).Include(sel => sel.Deals).ThenInclude(deal => deal.Product).ToList();
            return Ok(sellers);
        }

        [HttpGet("{selId}")]
        public async Task<IActionResult> GetByPhone(int selId)
        {
            var seller = _shopContext.Sellers.Include(sel => sel.OutletSection).Include(sel => sel.Deals).FirstOrDefault(sel => sel.selId == selId);
            return Ok(seller);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Seller seller)
        {
            _shopContext.Sellers.Add(seller);
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{selId}")]
        public async Task<IActionResult> Put(int selId, Seller _seller)
        {
            var seller = _shopContext.Sellers.FirstOrDefault(sel => sel.selId == selId);
            if (seller == null)
            {
                return NotFound();
            }
            seller.firstName = _seller.firstName;
            seller.secondName = _seller.secondName;
            seller.phoneNumber = _seller.phoneNumber;
            seller.patrynomic = _seller.patrynomic;
            seller.endOfContract = _seller.endOfContract;
            seller.salary = _seller.salary;
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{selId}")]
        public async Task<IActionResult> Delete(int selId)
        {
            var deletedSeller = _shopContext.Sellers.FirstOrDefault(sel => sel.selId == selId);
            _shopContext.Sellers.Remove(deletedSeller);
            _shopContext.SaveChanges();
            return Ok();
        }
    }
}
