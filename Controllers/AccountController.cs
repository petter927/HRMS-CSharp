using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Skygate_AspNet_MVC.Models;
using Skygate_AspNet_MVC.Utilities;
using System.Security.Claims;

public class AccountController : Controller
{
    private readonly SkyGateContext _context;
    public AccountController(SkyGateContext context) => _context = context;

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string account, string password)
    {
        // 將密碼進行 SHA256 加密後與資料庫比對
        string hashedPassword = HashHelper.ToSHA256(password);
        
        // 透過 EF Core 進行多表關聯查詢 (Include) 取得使用者與角色
        var user = await _context.SysAccounts
            .Include(a => a.Emp)
                .ThenInclude(e => e.Roles) 
            .FirstOrDefaultAsync(a => a.Account == account && a.Password == hashedPassword && a.IsActive == true);

        if (user != null)
        {            
            // 配置 Claims 並實作 Cookie-based Authentication 登入
            var roles = user.Emp.Roles.Select(r => r.RoleName).ToList();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Emp.EmpName),
                new Claim("EmpID", user.EmpId)
            };

            foreach (var roleName in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "帳號或密碼錯誤";
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}