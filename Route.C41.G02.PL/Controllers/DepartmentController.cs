﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.PL.ViewModels;
using System;
using System.Collections.Generic;

namespace Route.C41.G02.PL.Controllers
{
    // Inhertiance  : DepartmentController is  a Controller
    // Composition  : DepartmentController has a DepartmentRepository

    public class DepartmentController : Controller
    {
        private readonly IUniteOfWork _uniteOfWork;

        // private readonly IDepartmentRepository _departmentRepo; // NULL
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public DepartmentController(
            IUniteOfWork uniteOfWork,
            //IDepartmentRepository departmentRepo,
            IWebHostEnvironment env, 
            IMapper mapper) // Ask SLR fro creating an object from class implmenting IDepartmentRepository
        {
            _uniteOfWork = uniteOfWork;
            // _departmentRepo = departmentRepo;
            _env = env;
            _mapper = mapper;
        }

        // /Department/Index
        public IActionResult Index()
        {
            var deparment = _uniteOfWork.DepartmentRepository.GetAll();

            var mappedDept = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(deparment);

            return View(mappedDept);
        }
        //  /Department/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
            if (ModelState.IsValid)  // Server Side Validation
            {
              
             _uniteOfWork.DepartmentRepository.Add(mappedDept);

                var count = _uniteOfWork.Complete();
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        // /Department/Details/10
        // /Department/Details
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var department = _uniteOfWork.DepartmentRepository.Get(id.Value);

            var mappedDept = _mapper.Map<Department, DepartmentViewModel>(department);
            if (department is null)
                return NotFound(); // 404


            return View(viewName, mappedDept);
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
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest(/*"Here is Error Okey :("*/);

            if (!ModelState.IsValid)
                return View(departmentVM);

            try
            {
                var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _uniteOfWork.DepartmentRepository.Update(mappedDept);
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
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Department");

                return View(departmentVM);
            }


        }

        [HttpGet]

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete (DepartmentViewModel departmentVM)
        {
            /// if(!ModelState.IsValid)
            ///     return BadRequest();

            try
            {
                var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _uniteOfWork.DepartmentRepository.Delete(mappedDept);
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
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the Department");

                return View(departmentVM);
            }

        }
    }
}
