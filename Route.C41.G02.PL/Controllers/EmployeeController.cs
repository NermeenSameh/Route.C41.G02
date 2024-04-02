using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G01.BLL.Repositories;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.PL.Helpers;
using Route.C41.G02.PL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace Route.C41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUniteOfWork _uniteOfWork;

        // private readonly IEmployeeRepository _employeeRepo; // NULL
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;




        //  private readonly IDepartmentRepository _departmentRepo;
        public EmployeeController(
            IUniteOfWork uniteOfWork,
            //IEmployeeRepository employeeRepo,
            IWebHostEnvironment env,
            IMapper mapper
            )
        {
            // _employeeRepo = employeeRepo;
            _uniteOfWork = uniteOfWork;
            _env = env;
            _mapper = mapper;
            //  _departmentRepo = departmentRepo;
        }

        public IActionResult Index(string searchInp)
        {
            var employee = Enumerable.Empty<Employee>();
            var employeeRepo = _uniteOfWork.Repository<Employee>() as EmployeeRepository;
            TempData.Keep();
            // Binding Through View's Dictionary : Tranfer Data from Action to View  [One Way]

            /// 1. ViewData
            /// =>  is a Dictionary Type Property (introduced in Asp.Net Framework 3.5)
            /// =>  It helps us to tranfer the data from controller [Action] to view
            /// //ViewData["Massage"] = "Hello From ViewData";

            /// 2. ViewBag
            /// =>  is a Dynamic Type Property (introduced in Asp.Net Framework 4.0 based on dynmaic Feature)
            /// =>  It helps us to transfer the data fromcontroller [Action] to view
            /// //ViewBag.Massage = "Hello from ViewBag";

            if (string.IsNullOrEmpty(searchInp))
                employee = employeeRepo.GetAll();
            else
                employee = employeeRepo.SearchByName(searchInp.ToLower());

            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);

            return View(mappedEmp);


        }

        [HttpGet]
        public IActionResult Create()
        {

            /// ViewData["Departments"] = _departmentRepo.GetAll();
            ///ViewBag.Departments = _departmentRepo.GetAll();

            return View();

        }

        [HttpPost]

        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            // Manual Mapping

            ///  var mappedEmp = new Employee()
            ///  {
            ///      Name = employeeVM.Name,
            ///      Age = employeeVM.Age,
            ///      Address= employeeVM.Address,
            ///      Salary = employeeVM.Salary,
            ///      Email = employeeVM.Email,
            ///      PhoneNumber = employeeVM.PhoneNumber,
            ///      IsActive = employeeVM.IsActive,
            ///      IsDeleted = employeeVM.IsDeleted,
            ///      HiringDate = employeeVM.HiringDate,
            ///      Gender = employeeVM.Gender,
            ///      EmployeeType = employeeVM.EmployeeType,
            ///      CreationDate = employeeVM.CreationDate,
            ///      DepartmentId = employeeVM.DepartmentId,
            ///  }


            if (ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");

                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);


                _uniteOfWork.Repository<Employee>().Add(MappedEmp);

                // 3. TempData 
                /// Update
                /// _uniteOfWork.EmployeeRepository.Update(employee);
                /// Delete
                /// _uniteOfWork.EmployeeRepository.Remove(employee);

                var count = _uniteOfWork.Complete();

                if (count > 0)
                    TempData["Message"] = "Employee is Create Succssfully";
                else
                    TempData["Message"] = "An Error Has Occured , Employee Not Created :(";

                return RedirectToAction(nameof(Index));



            }
            return View(employeeVM);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();

            var emp = _uniteOfWork.Repository<Employee>().Get(id.Value);

            var mapped = _mapper.Map<Employee, EmployeeViewModel>(emp);

            if (emp is null)
                return NotFound();

            if (viewName.Equals("Delete", StringComparison.OrdinalIgnoreCase))
                TempData["ImageName"] = emp.ImageName;

            return View(viewName, mapped);


        }


        public IActionResult Edit(int? id)
        {
            //  ViewData["Departments"] = _departmentRepo.GetAll();

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {

            if (id != employeeVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employeeVM);

            try
            {
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _uniteOfWork.Repository<Employee>().Update(MappedEmp);
                _uniteOfWork.Complete();
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

                return View(employeeVM);
            }
        }

        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {


            try
            {
                employeeVM.ImageName = TempData["ImageName"] as string;
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _uniteOfWork.Repository<Employee>().Delete(MappedEmp);
                var count = _uniteOfWork.Complete();
                if (count > 0)
                {
                    DocumentSettings.DeleteFile(employeeVM.ImageName , "images");
                    return RedirectToAction(nameof(Index));
                }
               return View(employeeVM);
            }
            catch (Exception ex)
            {
                // 1. Log Exception 
                // 2. Friendly Message

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the Employee");

                return View(employeeVM);
            }

        }

    }
}
