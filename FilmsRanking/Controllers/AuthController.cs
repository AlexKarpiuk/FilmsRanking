using FilmsRanking.Data;
using FilmsRanking.Interfaces;
using FilmsRanking.Models;
using FilmsRanking.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FilmsRanking.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly ISanitizerService _sanitizerService;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            ApplicationDbContext context, IConfiguration config, ISanitizerService sanitizerService)
        {
            _context = context;
            _config = config;
            _sanitizerService = sanitizerService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var sanitizedEmail = _sanitizerService.SanitizeHtml(loginViewModel.EmailAddress);
            var sanitizedPassword = _sanitizerService.SanitizeHtml(loginViewModel.Password);

            var user = await _userManager.FindByEmailAsync(sanitizedEmail);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, sanitizedPassword); ;
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, sanitizedPassword, false, false);
                    if (result.Succeeded)
                    {
                        var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
                        var claims = new List<Claim>()
                        {
                            new Claim("email", sanitizedEmail),
                            new Claim("role", userRole),
                        };

                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

                        var signingCred = new SigningCredentials(securityKey, 
                            SecurityAlgorithms.HmacSha256);

                        var securityToken = new JwtSecurityToken(
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(20),
                            issuer: _config.GetSection("Jwt:Issuer").Value,
                            audience: _config.GetSection("Jwt:Audience").Value,
                            signingCredentials: signingCred);

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
                        await Console.Out.WriteLineAsync($"JWT: {tokenString}");
                        HttpContext.Session.SetString("JWToken", tokenString);

                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, check your password";
                return View(loginViewModel);
            }

            TempData["Error"] = "Wrong credentials. Please, check your email or password";
            return View(loginViewModel);
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var sanitizedEmail = _sanitizerService.SanitizeHtml(registerViewModel.EmailAddress);
            var sanitizedPassword = _sanitizerService.SanitizeHtml(registerViewModel.Password);
            var sanitizedPhoneNumber = _sanitizerService.SanitizePhoneNumber(registerViewModel.PhoneNumber);

            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(sanitizedEmail);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                Email = sanitizedEmail,
                UserName = sanitizedEmail,
                PhoneNumber = sanitizedPhoneNumber,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, sanitizedPassword);
            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            else
            {
                TempData["Error"] = "Password should contain at least one digit, special character and uppercase symbol";
                return View(registerViewModel);
            }

            return RedirectToAction("Index", "Movie");
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
