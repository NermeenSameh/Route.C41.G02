using Route.C41.G02.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Route.C41.G02.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        //[Requiresd (ErrorMessage = "Code is Required ya Nemoo!!")]
        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; }
        // [Required]
        [Required(ErrorMessage = "Code is Required")]
        public string Name { get; set; }
        [Display(Name = "Date of Creation")]
        public DateTime DataOfCreation { get; set; }

        // Navigational Property [Many]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    }
}
