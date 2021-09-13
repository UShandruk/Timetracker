using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Timetracker.Models;
using Timetracker.RolesApp.Models;

namespace Timetracker.Controllers
{
    public class AuthController : Controller
    {
        private ApplicationContext _context;
        public AuthController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await _context.Employees.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (employee == null)
                {
                    // добавляем пользователя в бд
                    employee = new Employee { Login = model.Login, Password = model.Password };
                    Role employeeRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    if (employeeRole != null)
                        employee.Role = employeeRole;

                    _context.Employees.Add(employee);
                    await _context.SaveChangesAsync();

                    await Authenticate(employee); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Неверный логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await _context.Employees
                    .Include(e => e.Role)
                    .FirstOrDefaultAsync(e => e.Login == model.Login && e.Password == model.Password);
                //Employee employee = await _context.Employees.Include(e => e.Role)
                //    .Where(e => e.Login == model.Login && e.Password == model.Password).FirstOrDefaultAsync();
                if (employee != null)
                {
                    await Authenticate(employee); //
                    if (employee.RoleId == 1)
                    {
                        return RedirectToAction("AdminPage", "Home");
                    }
                    else if (employee.RoleId == 2)
                    {
                        return RedirectToAction("UserPage", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Home");
                    }
                }
                ModelState.AddModelError("", "Неверный логин и(или) пароль");
            }
            return View(model);
        }

        /// <summary>
        /// Аутентификация
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <returns></returns>
        private async Task Authenticate(Employee employee)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, employee.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, employee.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
