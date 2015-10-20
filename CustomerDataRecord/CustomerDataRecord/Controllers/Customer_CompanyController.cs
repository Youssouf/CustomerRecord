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
    public class Customer_CompanyController : Controller
    {
        private CustomerDataEntities db = new CustomerDataEntities();

        // GET: Customer_Company
        public ActionResult Index()
        {
            var customer_Company = db.Customer_Company.Include(c => c.Company).Include(c => c.Customer);            
            return View(customer_Company.ToList());
        }

        // GET: Customer_Company/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Company customer_Company = db.Customer_Company.Find(id);
            if (customer_Company == null)
            {
                return HttpNotFound();
            }
            return View(customer_Company);
        }

        // GET: Customer_Company/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "CompanyName");
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "FirstName");
            return View();
        }

        // POST: Customer_Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer_Company customer_Company)
        {
            if (ModelState.IsValid)
            {
                db.Customer_Company.Add(customer_Company);
                db.SaveChanges();
                return RedirectToAction("Index","Customers");
            }

            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "CompanyName", customer_Company.CompanyID);
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "FirstName", customer_Company.CustomerID);
            return View(customer_Company);
        }

        // GET: Customer_Company/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Company customer_Company = db.Customer_Company.Find(id);
            if (customer_Company == null)
            {
                return HttpNotFound();
            }
             
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "CompanyName", customer_Company.CompanyID);
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "FirstName", customer_Company.CustomerID);
            return View(customer_Company);
        }

        // POST: Customer_Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Customer_Company customer_Company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_Company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.CompanyID = new SelectList(db.Company, "CompanyID", "CompanyName", customer_Company.CompanyID);
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "FirstName", customer_Company.CustomerID);
            return View(customer_Company);
        }

        // GET: Customer_Company/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Company customer_Company = db.Customer_Company.Find(id);
            if (customer_Company == null)
            {
                return HttpNotFound();
            }
            return View(customer_Company);
        }

        // POST: Customer_Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer_Company customer_Company = db.Customer_Company.Find(id);
            db.Customer_Company.Remove(customer_Company);
            db.SaveChanges();
            return RedirectToAction("Index");
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
