using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G02.DAL.Models;
using System;

namespace Route.C41.G02.PL.Controllers
{
    // Inhertiance  : DepartmentController is  a Controller
    // Composition  : DepartmentController has a DepartmentRepository

    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo; // NULL
        private readonly IWebHostEnvironment _env;
        public DepartmentController(IDepartmentRepository departmentRepo, IWebHostEnvironment env) // Ask SLR fro creating an object from class implmenting IDepartmentRepository
        {
            _departmentRepo = departmentRepo;
            _env = env;
        }

        // /Department/Index
        public IActionResult Index()
        {
            var deparment = _departmentRepo.GetAll();

            return View(deparment);
        }
        //  /Department/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {

            if (ModelState.IsValid)  // Server Side Validation
            {
                var count = _departmentRepo.Add(department);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // /Department/Details/10
        // /Department/Details
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var department = _departmentRepo.Get(id.Value);

            if (department is null)
                return NotFound(); // 404


            return View(viewName, department);
        }


        // /Department/Edit/10
        // /Department/Edit

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            /// if(!id.HasValue)
            ///     return BadRequest(); // 400
            /// var department = _departmentRepo.Get(id.Value);
            ///
            /// if(department is null)
            ///     return NotFound(); // 404 
            ///
            /// return View(department);

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest(/*"Here is Error Okey :("*/);

            if (!ModelState.IsValid)
                return View(department);

            try
            {
                _departmentRepo.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception 
                // 2. Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Department");

                return View(department);
            }


        }
    
    
    
    }
}
