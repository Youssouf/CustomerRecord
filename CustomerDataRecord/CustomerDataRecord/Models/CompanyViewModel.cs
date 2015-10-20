using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CustomerDataRecord.Models
{
    [MetadataType(typeof(CompanyViewModel))]
    
    public partial class Company
    {

    }
    
    class CompanyViewModel
    {
        
        public int CompanyID { get; set; }


        [Required(ErrorMessage=" A Company Name is required")]
        [DisplayName("C. Name")]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The address must be less than 30 characters.")]
        [DisplayName("Address")]
        public string CompanyAddress { get; set; }

        [Required(ErrorMessage = "The Country must be provided")]
        [DisplayName("Country")]
        
        [StringLength(15, ErrorMessage = "The number of string must be lessthant 15 charcters.")]

        public string Country { get; set; }
        
    }
}
