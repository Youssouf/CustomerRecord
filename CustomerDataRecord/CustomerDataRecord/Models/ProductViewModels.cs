using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomerDataRecord.Models
{
    [MetadataType(typeof(ProductViewModels))]

    public partial class Product
    {
        //
    }
    
    public  class ProductViewModels
    {
        [DisplayName("Product Name")]
        [Required(ErrorMessage ="Product name is required")]
        public string ProductName { get; set; }
        [Required]
        [DisplayName("Unit Price")]
        public Nullable<decimal> UnitePrice { get; set; }

        public Nullable<int> Quantity { get; set; }

        [DisplayName("Units In Stock")]
        public Nullable<int> UnitsInStock { get; set; }

        [DisplayName("Units on Order")]
        public Nullable<int> UnitsOnOrder { get; set; }

        [DisplayName("Product Categories")]
        public Nullable<int> CategoryID { get; set; }
    }
}
