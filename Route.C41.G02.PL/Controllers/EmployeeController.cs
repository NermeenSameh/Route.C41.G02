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
        //  private readonly IDepartmentRepository _departmentRepo;
        public EmployeeController(IEmployeeRepository employeeRepo, IWebHostEnvironment env /*,IDepartmentRepository departmentRepo*/)
        {
            _employeeRepo = employeeRepo;
            _env = env;
            //  _departmentRepo = departmentRepo;
        }

        public IActionResult Index()
        {
            TempData.Keep();
            // Binding Through View's Dictionary : Tranfer Data from Action to View  [One Way]

            // 1. ViewData
            // =>  is a Dictionary Type Property (introduced in Asp.Net Framework 3.5)
            // =>  It helps us to tranfer the data from controller [Action] to view

            ViewData["Massage"] = "Hello From ViewData";

            // 2. ViewBag
            // =>  is a Dynamic Type Property (introduced in Asp.Net Framework 4.0 based on dynmaic Feature)
            // =>  It helps us to transfer the data fromcontroller [Action] to view

            ViewBag.Massage = "Hello from ViewBag";
            var employee = _employeeRepo.GetAll();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {

            // ViewData["Departments"] = _departmentRepo.GetAll();
            //ViewBag.Departments = _departmentRepo.GetAll();
            return View();

        }

        [HttpPost]

        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = _employeeRepo.Add(employee);

                // 3. TempData 

                if (count > 0)
                    TempData["Message"] = "Employee is Create Succssfully";

                else
                    TempData["Message"] = "An Error Has Occured , Employee Not Created :(";

                return RedirectToAction(nameof(Index));

            }
            return View(employee);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();

            var emp = _employeeRepo.Get(id.Value);

            if (emp is null)
                return NotFound();

            return View(viewName, emp);


        }


        public IActionResult Edit(int? id)
        {
            //  ViewData["Departments"] = _departmentRepo.GetAll();

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, Employee employee)
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
