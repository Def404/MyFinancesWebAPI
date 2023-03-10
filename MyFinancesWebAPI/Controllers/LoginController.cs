using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MyFinancesContext _context;

        public LoginController(MyFinancesContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] UserLogin userLogin)
        {
            User? user;

            await using (MyFinancesContext db = new MyFinancesContext())
            {
                user = db.Users
                    .FromSqlRaw("SELECT * FROM users WHERE login = {0} AND password = crypt({1}, password)",
                        userLogin.Login,
                        userLogin.Password)
                    .FirstOrDefault();
            }

            if (user == null)
                return NotFound("Пользователь не найден");

            var securityKey = AuthOptions.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, user.Login)
            };
                
            var token = new JwtSecurityToken(
                AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: credentials);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
            });

        }
    }
}
