using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerDataRecord.Models;
using System.IO;

namespace CustomerDataRecord.Controllers
{
    
    public class EmployeesController : Controller
    {
        private CustomerDataEntities db = new CustomerDataEntities();

        // GET: Employees
        public ActionResult Index()
        {
          //  var employees = db.Employees.Include(e => e.Company);

            var emp = (from emp1 in db.Employees.Include(e => e.Company)
                       select emp1).ToList();
            return View(emp);
            
          //  return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "CompanyName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "CompanyName", employees.CompanyID);
            return View(employees);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "CompanyName", employees.CompanyID);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "CompanyName", employees.CompanyID);
            return View(employees);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees employees = db.Employees.Find(id);
            db.Employees.Remove(employees);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


     public  PartialViewResult GetEmployeeDetails(int id)
        {
            var emp = from employee in db.Employees
                      where employee.EmployeeID == id
                      select employee;

            Employees empl = emp.FirstOrDefault();

            return PartialView("_EmployeDetails", empl);

        }


    //    public void  FileUpload(HttpPostedFileBase file)
    //{
    //    if (file != null)
    //    {
    //        string pic = System.IO.Path.GetFileName(file.FileName);
    //        string path = System.IO.Path.Combine(
    //                               Server.MapPath("~/Content/Images"), pic);
    //        // file is uploaded
    //        file.SaveAs(path);
 
    //        // save the image path path to the database or you can send image
    //        // directly to database
    //        // in-case if you want to store byte[] ie. for DB
    //        using (MemoryStream ms = new MemoryStream())
    //        {
    //             file.InputStream.CopyTo(ms);
    //             byte[] array = ms.GetBuffer();
    //        }
 
    //    }
    //    // after successfully uploading redirect the user
    //   // return RedirectToAction("Index", "Employees");
    //}






        [HttpGet]
        public ActionResult UploadPhoto()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult UploadPhoto(HttpPostedFileBase file)
        {
            PhotoRenderHelper photo = new PhotoRenderHelper();
            photo.UploadPhotoToDB(file);
            return PartialView(file);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
