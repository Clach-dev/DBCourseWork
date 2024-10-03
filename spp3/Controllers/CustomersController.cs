using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spp3.Data;
using spp3.Data.Models;

namespace spp3.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ShopContext _shopContext;
        public CustomerController(ILogger<CustomerController> logger, ShopContext context)
        {
            _shopContext = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = _shopContext.Customers.Include(cu => cu.BonusCard).Include(pr => pr.Deals).ThenInclude(dl => dl.Product).ToList();
            return Ok(customers);
        }

        [HttpGet("{cuId}")]
        public async Task<IActionResult> GetByPhone(int cuId)
        {
            var customer = _shopContext.Customers.Include(cu => cu.BonusCard).Include(pr => pr.Deals).ThenInclude(dl => dl.Product).FirstOrDefault(cu => cu.cuId == cuId);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Customer customer)
        {
            _shopContext.Customers.Add(customer);
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{cuId}")]
        public async Task<IActionResult> Put(int cuId, Customer _customer)
        {
            var customer = _shopContext.Customers.FirstOrDefault(cu => cu.cuId == cuId);
            customer.firstName = _customer.firstName;
            customer.secondName = _customer.secondName;
            customer.patrynomic = _customer.patrynomic;
            customer.phoneNumber = _customer.phoneNumber;
            customer.adress = _customer.adress;
            customer.age = _customer.age;
            customer.gender = _customer.gender;
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{cuId}")]
        public async Task<IActionResult> Delete(int cuId)
        {
            var deletedCustomer = _shopContext.Customers.FirstOrDefault(cu => cu.cuId == cuId);
            _shopContext.Customers.Remove(deletedCustomer);
            _shopContext.SaveChanges();
            return Ok();
        }
    }
}
