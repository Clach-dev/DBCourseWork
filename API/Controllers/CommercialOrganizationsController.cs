using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spp3.Data;

namespace spp3.Controllers
{
    [ApiController]
    [Route("commercialorganizations")]
    public class CommercialOrganizationsController : Controller
    {
        private readonly ILogger<CommercialOrganizationsController> _logger;
        private readonly ShopContext _shopContext;
        public CommercialOrganizationsController(ILogger<CommercialOrganizationsController> logger, ShopContext context)
        {
            _shopContext = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var comOrgs = _shopContext.CommercialOrganizations.Include(co => co.TradeOutlets).ToList();
            return Ok(comOrgs);
        }

        [HttpGet("{coId}")]
        public async Task<IActionResult> GetByName(int coId)
        {
            var comOrg = _shopContext.CommercialOrganizations.Include(cu => cu.TradeOutlets).FirstOrDefault(co => co.coId == coId);
            return Ok(comOrg);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CommercialOrganization comOrg)
        {
            _shopContext.CommercialOrganizations.Add(comOrg);
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{coId}")]
        public async Task<IActionResult> Put(int coId, CommercialOrganization _comOrg)
        {
            var comOrg = _shopContext.CommercialOrganizations.FirstOrDefault(co => co.coId == coId);


            if (comOrg == null)
            {
                return NotFound();
            }

            comOrg.organizationName = _comOrg.organizationName;
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedComOrg = _shopContext.CommercialOrganizations.FirstOrDefault(co => co.coId == id);
            _shopContext.CommercialOrganizations.Remove(deletedComOrg);
            _shopContext.SaveChanges();
            return Ok();
        }
    }
}
