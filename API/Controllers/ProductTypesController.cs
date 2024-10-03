using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spp3.Data;
using spp3.Data.Models;

namespace spp3.Controllers
{
    [ApiController]
    [Route("producttypes")]
    public class ProductTypesController : Controller
    {
        private readonly ShopContext _shopContext;
        private readonly ILogger<ProductTypesController> _logger;

        public ProductTypesController(ILogger<ProductTypesController> logger, ShopContext shopContext)
        {
            _shopContext = shopContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productTypes = _shopContext.ProductTypes.Include(pt => pt.Products).ToList();
            return Ok(productTypes);
        }

        [HttpGet("{ptId}")]
        public async Task<IActionResult> GetByName(int ptId)
        {
            var productType = _shopContext.ProductTypes.Include(pt => pt.Products).FirstOrDefault(pt => pt.ptId == ptId);
            return Ok(productType);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductType productType)
        {
            _shopContext.ProductTypes.Add(productType);
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{ptId}")]
        public async Task<IActionResult> Put(int ptId, ProductType _productType)
        {
            var productType = _shopContext.ProductTypes.FirstOrDefault(pt => pt.ptId == ptId);
            productType.name = _productType.name;
            productType.ageLimit = _productType.ageLimit;
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{ptId}")]
        public async Task<IActionResult> Delete(int ptId)
        {
            var productType = _shopContext.ProductTypes.FirstOrDefault(pt => pt.ptId == ptId);
            _shopContext.ProductTypes.Remove(productType);
            _shopContext.SaveChanges();
            return Ok();
        }

    }
}
