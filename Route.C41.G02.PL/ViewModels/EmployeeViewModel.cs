using Route.C41.G02.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Route.C41.G02.PL.ViewModels
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Lenght of Name is 50 Chars")]
        [MinLength(5, ErrorMessage = " Min Lenght of Name is 5 Chars")]
        public string Name { get; set; }

        [Range(22, 30)]
        public int? Age { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }
        public EmpType EmployeeType { get; set; }

        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        public int? DepartmentId { get; set; }
        public Department Departments { get; set; }

    }
}
