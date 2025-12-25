using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using School.Data;
using School.Services;

namespace School.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly LoginTokenService _tokenService;

        public AuthController(ApplicationDbContext context, LoginTokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpGet("signin")]
        public async Task<IActionResult> SignInWithToken([FromQuery] string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest("Token is required.");
                }
                var userId = _tokenService.ValidateAndConsume(token);
                if (!userId.HasValue)
                {
                    return BadRequest("Invalid or expired token.");
                }

                var user = await _context.Users.FindAsync(userId.Value);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim("PhoneNumber", user.PhoneNumber ?? string.Empty)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

                return Redirect("/");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SignInWithToken: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("signout")]
        public async Task<IActionResult> SignOutAndRedirect()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Redirect("/login");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SignOutAndRedirect: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}