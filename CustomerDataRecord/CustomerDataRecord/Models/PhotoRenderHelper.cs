using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CustomerDataRecord.Models;

namespace CustomerDataRecord.Models
{
 public  class PhotoRenderHelper
    {
        CustomerDataEntities db = new CustomerDataEntities();
        public void  UploadPhotoToDB(HttpPostedFileBase file)
        {
            Employees emp = new Employees();
            emp.Photo = ConvertImageToByte(file);
            db.Employees.Add(emp);
            db.SaveChanges();         
        }
        public byte[] ConvertImageToByte(HttpPostedFileBase image)
        {
            byte[] imageByte = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageByte = reader.ReadBytes((int)image.ContentLength);
            return imageByte;
        }
    }
}
