using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CustomerDataRecord.Models
{
    [MetadataType(typeof(EmployeeViewModels))]


  public  partial class Employees
    {

    }
    class EmployeeViewModels
    {

        [DisplayName("First Name")]
        [Required(ErrorMessage= "The first name is required")]
        public string FirstName { get; set; }


        [DisplayName("Last Name")]
        [Required(ErrorMessage = "The last name is required")]
        public string LastName { get; set; }

        [DisplayName("Department")]
        [Required]
        public string DepName { get; set; }
        
       
        public string Title { get; set; }
        public string Age { get; set; }
        [Required]
        [DisplayFormat(DataFormatString= "{0:dd MMM yyyy}")]
        [DisplayName("Start date")]
        public Nullable<System.DateTime> HireDate { get; set; }

        [DisplayFormat(DataFormatString ="{0:C}")]
        public Nullable<decimal> Salary { get; set; }

        //public string Gender { get; set; }
        //public string Address { get; set; }
        //public string City { get; set; }
        //public string Region { get; set; }
        //public string Coountry { get; set; }
        public byte[] Photo { get; set; }
    }
}
