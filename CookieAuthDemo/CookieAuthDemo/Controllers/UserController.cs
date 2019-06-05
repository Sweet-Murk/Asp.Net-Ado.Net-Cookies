using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookieAuthDemo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieAuthDemo.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        EmployeeDataAccessLayer objemployee = new EmployeeDataAccessLayer();
        public IActionResult UserHome()
        {
            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee = objemployee.GetAllEmployees().ToList();
            return View(lstEmployee);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = objemployee.GetEmployeeData(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objemployee.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = objemployee.GetEmployeeData(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("UserLogin", "Login");
        }
    }
}