using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Timetracker.Models;
using Timetracker.RolesApp.Models;

namespace Timetracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AdminPage()
        {
            List<Employee> employees = _context.Employees
                    .Include(e => e.Role).ToList();
            //.FirstOrDefaultAsync(e => e.Login == model.Login && e.Password == model.Password);
            return View(employees);
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult UserPage()
        {
            return View();
        }

        //[Authorize(Roles = "admin")]
        //public IActionResult About()
        //{
        //    return Content("Вход только для администратора");
        //}

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult BlockEmployee(/*int employeeId*/)
        {
            int id = Int32.Parse(Request.Query.FirstOrDefault(p => p.Key == "Id").Value);
            Employee employee = _context.Employees.Where(x => x.Id == id).FirstOrDefault();
            employee.RoleId = 3;
            _context.Update(employee);

            return View("AdminPage");
        }

        [Authorize(Roles = "admin")]
        public IActionResult BlockEmployees(int[] employeeId)
        {
            return View("AdminPage");
        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet]
        public IActionResult WorkStarted()
        {
            //TimeCounter.
            return View();
        }
    }
}
