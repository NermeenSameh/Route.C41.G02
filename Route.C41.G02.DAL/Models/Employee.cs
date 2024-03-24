using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.DAL.Models
{
  public  enum Gender
    {
        [EnumMember (Value="Male")]
        Male = 1 ,
        [EnumMember(Value = "Female")]
        Female = 2
    }
  public  enum EmpType
    {
        [EnumMember(Value = "FullTime")]
        FullTime = 1 ,
        [EnumMember(Value = "PartTime")]
        PartTime = 2 ,
    }
    public class Employee : ModelBase
    {
     
        
        [Required]
        [MaxLength(50 , ErrorMessage = "Max Lenght of Name is 50 Chars")]
        [MinLength(5 , ErrorMessage = " Min Lenght of Name is 5 Chars")]
        public string Name { get; set; }

        [Range(22 , 30)]
        public int? Age { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name="Hiring Date")]
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }
        public EmpType EmployeeType { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;   
    }
}
