using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        
        private ApplicationDbContext _context;

        // GET: Customers

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();    
        }

        public ActionResult Index()
        {
            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View();

        }

        public ActionResult Details(int Id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == Id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);

        }

        public ActionResult CustomerForm()
        {
            var membershiptypes = _context.MembershipTypes.ToList();

            var newCustomerViewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershiptypes
            };

            return View(newCustomerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customers customers)
        {
            if(!ModelState.IsValid)
            {
                var customerViewModel = new CustomerFormViewModel(customers)
                {
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", customerViewModel);
            }
            //New Customer
            if(customers.Id == 0)
                _context.Customers.Add(customers);
            //Existing Customer
            else
            {
                var existingCustomer = _context.Customers.Single(c => c.Id == customers.Id);

                existingCustomer.Name = customers.Name;
                existingCustomer.DateOfBirth = customers.DateOfBirth;
                existingCustomer.MembershipTypeId = customers.MembershipTypeId;
                existingCustomer.IsSubscribedToNewsLetter = customers.IsSubscribedToNewsLetter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int Id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (customer == null)
                return HttpNotFound();

            var customerViewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", customerViewModel);
        }
    }
}