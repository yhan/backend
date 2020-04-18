using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        readonly UserManager<IdentityUser> _userManager;
        readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Credentials credentials)
        {
            var user = new IdentityUser { UserName = credentials.Email, Email = credentials.Email };

            var result = await _userManager.CreateAsync(user: user, password: credentials.Password);

            if (!result.Succeeded)
                return BadRequest(error: result.Errors);

            await _signInManager.SignInAsync(user: user, isPersistent: false);

            SigningCredentials signingCredentials = new SigningCredentials(key: StrongBox.PassPhrase, algorithm: SecurityAlgorithms.HmacSha256Signature);
            var jwt = new JwtSecurityToken(issuer: null, audience: null, claims: null, notBefore: null, expires: null, signingCredentials: signingCredentials);
            return Ok(value: new JwtSecurityTokenHandler().WriteToken(token: jwt));
        }
    }
}