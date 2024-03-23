using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.DAL.Models
{
    // Model
    public class Department : ModelBase
    {
        
        //[Requiresd (ErrorMessage = "Code is Required ya Nemoo!!")]
        [Required(ErrorMessage ="Code is Required")]
        public string Code { get; set; }
        // [Required]
        [Required(ErrorMessage = "Code is Required")]
        public string Name { get; set; }
        [Display(Name = "Date of Creation")]
        public DateTime DataOfCreation { get; set; }


    }
}
