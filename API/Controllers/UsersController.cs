using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spp3.Data;

namespace spp3.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ShopContext _shopContext;
        public UsersController(ILogger<UsersController> logger, ShopContext context)
        {
            _shopContext = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = _shopContext.Users.Include(us => us.UserRole).ToList();
            return Ok(users);
        }

        [HttpGet("{usId}")]
        public async Task<IActionResult> GetByPhone(int usId)
        {
            var users = _shopContext.Users.Include(us => us.UserRole).FirstOrDefault(us => us.usId == usId);
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            _shopContext.Users.Add(user);
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{usId}")]
        public async Task<IActionResult> Put(int usId, User _user)
        {
            var user = _shopContext.Users.FirstOrDefault(us => us.usId == usId);

            if (user == null)
            {
                return NotFound();
            }

            user.login = _user.login;
            user.password = _user.password;
            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{usId}")]
        public async Task<IActionResult> Delete(int usId)
        {
            var deletedUser = _shopContext.Users.FirstOrDefault(us => us.usId == usId);
            _shopContext.Users.Remove(deletedUser);
            _shopContext.SaveChanges();
            return Ok();
        }
    }
}
