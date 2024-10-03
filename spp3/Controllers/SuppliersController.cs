using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spp3.Data;

namespace spp3.Controllers
{
    [ApiController]
    [Route("suppliers")]
    public class SupplierController : Controller
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ShopContext _shopContext;
        public SupplierController(ILogger<SupplierController> logger, ShopContext context)
        {
            _shopContext = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var suppliers = _shopContext.Suppliers.Include(su => su.OrdersToSuppliers).ThenInclude(ots => ots.Order).ThenInclude(or => or.ProductsToOrders).ThenInclude(pto => pto.Product).ToList();
            return Ok(suppliers);
        }

        [HttpGet("{suId}")]
        public async Task<IActionResult> GetByName(int suId)
        {
            var supplier = _shopContext.Suppliers.Include(su => su.OrdersToSuppliers).FirstOrDefault(su => su.suId == suId);
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Supplier supplier)
        {
            _shopContext.Suppliers.Add(supplier);
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{suId}")]
        public async Task<IActionResult> Put(int suId, Supplier _supplier)
        {
            var supplier = _shopContext.Suppliers.FirstOrDefault(su => su.suId == suId);
            if (supplier == null)
            {
                return NotFound();
            }
            supplier.supplierName = _supplier.supplierName;
            supplier.price = _supplier.price;
            supplier.quantity = _supplier.quantity;
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{suId}")]
        public async Task<IActionResult> Delete(int suId)
        {
            var deletedSupplier = _shopContext.Suppliers.FirstOrDefault(su => su.suId == suId);
            _shopContext.Suppliers.Remove(deletedSupplier);
            _shopContext.SaveChanges();
            return Ok();
        }
    }
}
