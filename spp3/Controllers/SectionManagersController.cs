using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spp3.Data;

namespace spp3.Controllers
{
    [ApiController]
    [Route("sectionmanagers")]
    public class SectionManagersController : Controller
    {
        private readonly ILogger<SectionManagersController> _logger;
        private readonly ShopContext _shopContext;
        public SectionManagersController(ILogger<SectionManagersController> logger, ShopContext context)
        {
            _shopContext = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sectionManagers = _shopContext.SectionManagers.Include(sm => sm.OutletSection).ToList();
            return Ok(sectionManagers);
        }

        [HttpGet("{smId}")]
        public async Task<IActionResult> GetByPhone(int smId)
        {
            var sectionManager = _shopContext.SectionManagers.Include(sm => sm.OutletSection).FirstOrDefault(sm => sm.smId == smId);
            return Ok(sectionManager);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SectionManager sectionManager)
        {
            _shopContext.SectionManagers.Add(sectionManager);
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{smId}")]
        public async Task<IActionResult> Put(int smId, SectionManager _sectionManager)
        {
            var sectionManager = _shopContext.SectionManagers.FirstOrDefault(sm => sm.smId == smId);
            if (sectionManager == null)
            {
                return NotFound();
            }
            sectionManager.firstName = _sectionManager.firstName;
            sectionManager.secondName = _sectionManager.secondName;
            sectionManager.phoneNumber = _sectionManager.phoneNumber;
            sectionManager.patrynomic = _sectionManager.patrynomic;
            sectionManager.experience = _sectionManager.experience;
            sectionManager.salary = _sectionManager.salary;

            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{smId}")]
        public async Task<IActionResult> Delete(int smId)
        {
            var deletedSectionManager = _shopContext.SectionManagers.FirstOrDefault(sm => sm.smId == smId);
            _shopContext.SectionManagers.Remove(deletedSectionManager);
            _shopContext.SaveChanges();
            return Ok();
        }
    }
}
