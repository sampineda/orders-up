using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrdersUpBackend.DataContext;
using OrdersUpBackend.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrdersUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly OrdersUpDataContext _database;
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private readonly ApplicationSettings _appSettings;

        public UserController(OrdersUpDataContext context, SignInManager<User> signInManager, UserManager<User> userManager, IOptions<ApplicationSettings> appSettings)
        {
            _database = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _database.Users.Include(q => q.Business).ToListAsync();
        }

        [HttpGet]
        [Route("businessInfo")]
        [Authorize]
        public async Task<Object> GetBusiness()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.BusinessId
            };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, User item)
        {
            if (string.IsNullOrEmpty(item.FullName))
            {
                return NotFound("Debe de ingresar un nombre valido");
            }
            else
            {
                _database.Entry(item).State = EntityState.Modified;
                await _database.SaveChangesAsync();

                return NoContent();
            }
        }

        [HttpPost]
        [Route("register")]
        //POST: api/user/Register
        public async Task<Object> PostUser(UserModel model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                BusinessId = model.BusinessId
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        //POST: api/user/login
        public async Task<IActionResult> Login  (LogIn model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user != null &&  await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {

                        new Claim("UserID", user.Id.ToString())
                    }),

                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "El nombre de usuario o contraseña es incorrecta." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var item = await _database.Users.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _database.Users.Remove(item);
            await _database.SaveChangesAsync();

            return NoContent();
        }
    }
}
