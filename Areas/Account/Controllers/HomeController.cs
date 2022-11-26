using Microsoft.AspNetCore.Mvc;

namespace MoviesApp.Areas.Accounts.Controllers
{
    [Area("Account")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("[area]/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("[area]/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                ModelState.AddModelError("", "Invalid details.");
                return View(model);
            }

            var res = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (res.Succeeded)
            {
                //return RedirectToAction("Index", "Home", new {Area=""});
                return Redirect("/");
            }
            ModelState.AddModelError("", "Invalid email / password");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = Guid.NewGuid().ToString().Replace("-", ""),
            };
            var res = await _userManager.CreateAsync(user, model.Password);

            if (res.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction(nameof(Login));
            }

            ModelState.AddModelError("", "An error occurred.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        public async Task<IActionResult> GenerateData()
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });

            var users = await _userManager.GetUsersInRoleAsync("Admin");
            if (users.Count == 0)
            {
                var appUser = new ApplicationUser()
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@admin.com",
                    UserName = "admin",
                };
                var res = await _userManager.CreateAsync(appUser, "Pass@123");
                await _userManager.AddToRoleAsync(appUser, "Admin");
            }
            return Ok("Data generated");
        }
    }
}
