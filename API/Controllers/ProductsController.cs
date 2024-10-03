using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spp3.Data;
using spp3.Data.Models;

namespace spp3.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _shopContext;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ShopContext shopCondext, ILogger<ProductsController> logger)
        {
            _shopContext = shopCondext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = _shopContext.Products.Include(pr => pr.ProductType).Include(pr => pr.Deals).Include(pr => pr.ProductsToOrders).ToList();
            return Ok(products);
        }

        [HttpGet("{prId}")]
        public async Task<IActionResult> GetByName(int prId)
        {
            var product = _shopContext.Products.Include(pr => pr.ProductType).Include(pr => pr.Deals).Include(pr => pr.ProductsToOrders).FirstOrDefault(pr => pr.prId == prId);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            _shopContext.Products.Add(product);
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{prId}")]
        public async Task<IActionResult> Put(int prId, Product _product)
        {
            var product = _shopContext.Products.FirstOrDefault(pr => pr.prId == prId);
            product.name = _product.name;
            product.price = _product.price;
            product.quantity = _product.quantity;
            product.ptId = _product.ptId;
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{prId}")]
        public async Task<IActionResult> Delete(int prId)
        {
            var product = _shopContext.Products.FirstOrDefault(pr => pr.prId == prId);
            _shopContext.Products.Remove(product);
            _shopContext.SaveChanges();
            return Ok();
        }
    }
}
