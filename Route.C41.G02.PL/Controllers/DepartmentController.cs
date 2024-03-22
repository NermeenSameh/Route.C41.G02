using Microsoft.AspNetCore.Mvc;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G02.DAL.Models;

namespace Route.C41.G02.PL.Controllers
{
    // Inhertiance  : DepartmentController is  a Controller
    // Composition  : DepartmentController has a DepartmentRepository

    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo; // NULL

        public DepartmentController(IDepartmentRepository departmentRepo) // Ask SLR fro creating an object from class implmenting IDepartmentRepository
        {
            _departmentRepo = departmentRepo;
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
             
            if(ModelState.IsValid)  // Server Side Validation
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
        public IActionResult Details (int? id)
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var department = _departmentRepo.Get(id.Value);

            if(department is null)
                return NotFound(); // 404


            return View(department);
        }
    }
}
