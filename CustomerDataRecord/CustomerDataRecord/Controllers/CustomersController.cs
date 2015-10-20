using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerDataRecord.Models;
using System.Xml;
using System.Web.UI.WebControls;
using System.Data.Entity.Core;

namespace CustomerDataRecord.Controllers
{   
    public class CustomersController : Controller
    {
        private CustomerDataEntities db = new CustomerDataEntities();
        
        /// <summary>
        ///   This method returns a list of customers     
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var cust = (from c in db.Customer
                       orderby c.FirstName, c.LastName ascending
                       select c).ToList();
            return View(cust);
        }
             

        /// <summary>
        /// This Method provides the details of a specific Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
           
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        /// <summary>
        /// Get function to create a Customer
        /// </summary>
        /// <returns></returns>
     
       [HttpGet]
        public ActionResult Create()
        {

            return View();// PartialView("_CreateCustomerPartial");
        }
      
        /// <summary>
        /// Post Method to create a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customer.Add(customer);
                db.SaveChanges();             
               return  RedirectToAction("Index", "Customers");
            }

            return View(customer);// PartialView("_CreateCustomerPartial", customer);
        }

       /// <summary>
       ///  Update the information of a customer
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
  
        public ActionResult Edit(int id)
        {
            Session["CustomerID"] = id;  // Important for handling concurrencyException

            Customer customer = GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
           // return PartialView("_EditCustomerPartial", customer);
            return View(customer); // PartialView(customer);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            customer.CustomerID = (int)Session["CustomerID"]; // To handle concurrencyException
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();  
                
               
                return RedirectToAction("Index");
            }
           // return PartialView("_EditCustomerPartial", customer);
            return PartialView(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            
            if (customer == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteCustomerPartial",customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
                db.Customer.Remove(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            
            }

        //public ActionResult GetView(int customerId, string v)
        //{
        //    object model = null;
        //    if (v == "CustomerDetails")
        //    {
        //        model = db.Customer.Find(customerId);
        //    }

        //    if (v == "OrderDetails")
        //    {
        //        var orderQuery = db.Orders.Where(orders => orders.CustomerID == customerId)
        //            .OrderBy(orders => orders.OrderID).ToList();
        //        model = orderQuery;
        //    }
        //    return PartialView(v, model);

        //}

       public PartialViewResult CustomerDetails(int id)
        {
       //   var custDatails = db.Customer.Where(x => x.CustomerID == id).ToList();
           var custDetail = from cust in db.Customer
                            where cust.CustomerID == id
                            select cust;
           return PartialView("CustomerDetails", custDetail);
        }


       public PartialViewResult GetCustomerOrderList(int id)
        {
           
            var queryOrder = (from ord in db.Customer
                              join ord1 in db.Orders on ord.CustomerID equals ord1.CustomerID
                              where ord.CustomerID == id
                              select ord1).ToList();

           if (queryOrder.Count != 0)
           {
               return PartialView("_CustomerOrderList", queryOrder);
           }
           else
           {
               return PartialView("_NoOrderFound");
           }

        }

        //public List<Customer> FindCustomerByName( string strnName)
        //{
        //    var searchCustomer = from cst in db.Customer
        //                         where cst.FirstName.Contains(strnName)
        //                         select cst;
        //    if (searchCustomer==null)
        //    {
               

        //    }
        //    return searchCustomer.ToList();

            // Try something here.

          //  var  queryCust = from c in db.Customer.Where(cust1 =>cust1.FirstName.Contains(strnName );
       // }

        public ActionResult FindCustomerByNameOrByCountry(string stringKey, string countryKey)
        {
            var listOfCountry = new List<string>();
           
            var searchCtry = from country in db.Customer
                             orderby country.Countrry
                             select country.Countrry;
            listOfCountry.AddRange(searchCtry.Distinct());

            ViewBag.countryKey = new SelectList(listOfCountry);

            var customSet = from cut in db.Customer
                            select cut;

            if (!string.IsNullOrEmpty(stringKey))
            {
                customSet = customSet.Where(x =>x.FirstName.Contains(stringKey));
                
                
            }

            if (!string.IsNullOrEmpty(countryKey))
            {
                customSet = customSet.Where(p => p.Countrry == countryKey);
            }
            return View(customSet);
                    
        }
       /// <summary>
       ///  Find all Companies for a given customer.
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>

       

        public PartialViewResult FindCompanyForCustomerPartial(int? id)
        {
            var companyList = (from cp in db.Company
                               join custComp in db.Customer_Company on cp.CompanyID equals custComp.CompanyID
                               join cut in db.Customer on custComp.CustomerID equals cut.CustomerID
                               where cut.CustomerID == id
                               select cp).ToList();

            if (companyList.Count != 0)
            {
                return PartialView("FindCompanyForCustomer", companyList);

            }
            else
            {

                return PartialView("_NoCompanyFound"); // View("NoCompany");
                // return View(companyList);
            }

        }

        /// <summary>
        ///  Find  a customer by unsing its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  Customer GetCustomerById(int id)
        {
            var queryCust = from cust in db.Customer.Where(cust => cust.CustomerID == id)                    
                            select cust;
            Customer newCustomer = queryCust.FirstOrDefault();

            return newCustomer;
        }
        public List<Customer> GetCustomerById()
        {
           var queryCust = from cust in db.Customer
                            select cust;

            var result = queryCust.GroupBy(test => test.CustomerID)
                   .Select(grp => grp.FirstOrDefault())
                   .ToList();

            return result;
        }
       public List<Company> GetCompanyName()
        {

            var querycompany = (from comp in db.Company
                              // where comp.CompanyName == companyName
                               select comp).ToList();


            return querycompany;
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
