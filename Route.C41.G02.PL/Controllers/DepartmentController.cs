using Microsoft.AspNetCore.Mvc;
using Route.C41.G01.BLL.Interfaces;

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
            return View();
        }
    }
}
