using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spp3.Data;
using spp3.Data.Models;

namespace spp3.Controllers
{
    [ApiController]
    [Route("bonuscards")]
    public class BonusCardsController : ControllerBase
    {
        private readonly ILogger<BonusCardsController> _logger;
        private readonly ShopContext _shopContext;
        public BonusCardsController(ILogger<BonusCardsController> logger, ShopContext context)
        {
            _shopContext = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = _shopContext.BonusCards.Include(bc => bc.Customer).ToList();

            return Ok(list);
        }


        [HttpGet("{bcId}")]
        public async Task<IActionResult> GetById(int bcId)
        {
            var bonusCard = _shopContext.BonusCards.Include(bc => bc.Customer).FirstOrDefault(bc => bc.bcId == bcId);
            return Ok(bonusCard);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BonusCard bonusCard)
        {
            _shopContext.BonusCards.Add(bonusCard);
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{bcId}")]
        public async Task<IActionResult> Put(int bcId, BonusCard _bonusCard)
        {
            var bonusCard = _shopContext.BonusCards.FirstOrDefault(bc => bc.bcId == bcId);
            bonusCard.number = _bonusCard.number;
            bonusCard.discount = _bonusCard.discount;
            bonusCard.cuId = _bonusCard.cuId;
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{bcId}")]
        public async Task<IActionResult> Delete(int bcId)
        {
            var deletedBonusCard = _shopContext.BonusCards.FirstOrDefault(bc => bc.bcId == bcId);
            _shopContext.BonusCards.Remove(deletedBonusCard);
            _shopContext.SaveChanges();
            return Ok();
        }

    }
}
