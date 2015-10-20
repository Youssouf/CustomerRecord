using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerDataRecord.Models;

namespace CustomerDataRecord.Controllers
{  
    public class CompaniesController : Controller
    {
        private CustomerDataEntities db = new CustomerDataEntities();


        // Get the  a list of Customer
        public ActionResult Index()
        {
            var compList = (from comp in db.Company
                            select comp).ToList();
            return View(compList);
        }

        // Get the details of a company
        public ActionResult Details(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = FindCompanyById(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
         // Create an inserte acompany to list.
        [HttpGet]
        public ActionResult Create()
        {
            return View();// PartialView("_CreateCompanyPartial");
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Company.Add(company);
                db.SaveChanges();
                return  RedirectToAction("Index");
            }

            return View(company);// PartialView("_CreateCompanyPartial", company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);// PartialView("_EditCompanyPartial", company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
               return RedirectToAction("Index", "Companies");
            }
            return View(company);// PartialView("_EditCompanyPartial", company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete",company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Company.Find(id);
            db.Company.Remove(company);
            db.SaveChanges();

            return RedirectToAction("Index");
            
        }

        public PartialViewResult FindCustomerForCompany(int id)
        {
            var customerList = (from cust in db.Customer
                                join custComp in db.Customer_Company on cust.CustomerID equals custComp.CustomerID
                                join cp in db.Company on custComp.CompanyID equals cp.CompanyID
                                where cp.CompanyID == id
                                select cust).ToList();
            
            if (customerList.Count == 0)
            {
                return PartialView("_NoCustomerFound");// View("NoCompany");
            }            
            return PartialView("_CustomerList", customerList);

        }
       public PartialViewResult GetCompanyEmployees(int id)
        {
            var queryEmp = (from comp in db.Company
                            join emp in db.Employees on comp.CompanyID equals emp.CompanyID
                            where comp.CompanyID == id
                            select emp).ToList();

            if (queryEmp.Count == 0)
            {
                return PartialView("_NoEmployeeFound");
            }
            else
            {
                return PartialView("_CompanyEmployees", queryEmp);
            }
            
        }

        public Company FindCompanyById(int ? id)
        {
            var queryComp = from cp in db.Company
                            where cp.CompanyID == id
                            select cp;
            Company company = queryComp.FirstOrDefault();

            return company;

        }

        public string FullName(Company comp) { return comp.CompanyName + " " + comp.Country; }

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
