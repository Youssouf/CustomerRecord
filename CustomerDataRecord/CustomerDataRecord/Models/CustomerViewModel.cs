using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace CustomerDataRecord.Models
{
    [MetadataType(typeof(CustomerViewModel))]

    public partial class Customer
    {

    }

    class CustomerViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [Required]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [DisplayName("Frist Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }


        [StringLength(50, ErrorMessage = "The address is required")]
        [Required(ErrorMessage = "Address is required")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please provide valid mail address")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]

        [DisplayName("Email")]
        public string EmailAddress { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "The Phone is required")]
        [RegularExpression(@"((\(\d{3}\)?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Phone number must be in the format xxx-xxx-xxxx")] //     @"((\(\d{3}\)?)|(\d{3}-))?\d{3}-\d{4}"
        public string Phone { get; set; }

        [Required(ErrorMessage = "City name is required")]
        [StringLength(20, ErrorMessage = "First name is required")]
        [DisplayName("City")]
        public string City { get; set; }

        [StringLength(50, ErrorMessage = "Country name  must be less than 50 characters")]
        [Required(ErrorMessage = "Counry name is required")]
        [DisplayName("Country")]
        public string Countrry { get; set; }


        //[DisplayName("Gender")]
        //[Required]
        //public string Gender { get; set; } 

    }
}
