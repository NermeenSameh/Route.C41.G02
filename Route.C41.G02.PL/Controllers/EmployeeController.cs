using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G02.DAL.Models;
using System;

namespace Route.C41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo; // NULL
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeeRepo, IWebHostEnvironment env)
        {
            _employeeRepo = employeeRepo;
            _env = env;
        }

        public IActionResult Index()
        {
            var employee = _employeeRepo.GetAll();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]

        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = _employeeRepo.Add(employee);
                if (count > 0)
                    return RedirectToAction(nameof(Index));

            }
            return View(employee);
        }

        public IActionResult Details(int? id , string viewName ="Details")
        {
            if (!id.HasValue)
                return BadRequest();

            var emp = _employeeRepo.Get(id.Value);

            if(emp is null)
                return NotFound();

            return View(viewName, emp);


        }


        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit ([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employee);

            try
            {
                _employeeRepo.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception 
                // 2. Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Employee");

                return View(employee);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
           

            try
            {
                _employeeRepo.Delete(employee);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception 
                // 2. Friendly Message

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the Employee");

                return View(employee);
            }

        }

    }
}
