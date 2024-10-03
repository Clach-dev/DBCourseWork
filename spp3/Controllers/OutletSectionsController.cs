using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spp3.Data;

namespace spp3.Controllers
{
    [ApiController]
    [Route("outletsections")]
    public class OutletSectionsController : Controller
    {
        private readonly ILogger<OutletSectionsController> _logger;
        private readonly ShopContext _shopContext;
        public OutletSectionsController(ILogger<OutletSectionsController> logger, ShopContext context)
        {
            _shopContext = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var outletSections = _shopContext.OutletSections.Include(os => os.TradeOutlet).Include(os => os.SectionManager).Include(os => os.Sellers).ToList();
            return Ok(outletSections);
        }

        [HttpGet("{osId}")]
        public async Task<IActionResult> GetByName(int osId)
        {
            var outletSection = _shopContext.OutletSections.Include(os => os.TradeOutlet).Include(os => os.SectionManager).Include(os => os.Sellers).FirstOrDefault(os => os.osId == osId);
            return Ok(outletSection);
        }

        [HttpPost]
        public async Task<IActionResult> Post(OutletSection outletSection)
        {
            _shopContext.OutletSections.Add(outletSection);
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{osId}")]
        public async Task<IActionResult> Put(int osId, OutletSection _outletSection)
        {
            var outletSection = _shopContext.OutletSections.FirstOrDefault(os => os.osId == osId);
            outletSection.sectionName = _outletSection.sectionName;
            outletSection.sectionFloor = _outletSection.sectionFloor;
            outletSection.sectionHall = _outletSection.sectionHall;
            outletSection.toId = _outletSection.toId;
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{osId}")]
        public async Task<IActionResult> Delete(int osId)
        {
            var deletedOutletSection = _shopContext.OutletSections.FirstOrDefault(os => os.osId == osId);
            _shopContext.OutletSections.Remove(deletedOutletSection);
            _shopContext.SaveChanges();
            return Ok();
        }
    }
}
