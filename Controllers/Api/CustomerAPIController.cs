using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTO;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomerAPIController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomerAPIController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/CustomerAPI/
        public IHttpActionResult GetCustomers()
        {
            var customerDTO = _context.Customers
                                    .Include(m => m.MembershipType)
                                    .ToList()
                                    .Select(Mapper.Map<Customers, CustomerDTO>);

            return Ok(customerDTO);
            
        }

        //GET /api/CustomerAPI/1
        public IHttpActionResult GetCustomers(int Id)
        {
            var customer = _context.Customers
                                .Include(m => m.MembershipType)
                                .SingleOrDefault(c => c.Id == Id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customers, CustomerDTO>(customer));
        }

        //POST /api/CustomerAPI
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDTO, Customers>(customerDTO);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDTO.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id),customerDTO);
        }

        //PUT /api/CustomerAPI/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int Id, CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var existingCustomer = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (existingCustomer == null)
                return NotFound();

            Mapper.Map(customerDTO, existingCustomer);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/CustomerAPI/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int Id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok();
        }
    }
}
