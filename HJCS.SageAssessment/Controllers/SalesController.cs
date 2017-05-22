using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HJCS.SageAssessment.Domain.Model;
using HJCS.SageAssessment.Domain.Repositories;

namespace HJCS.SageAssessment.WebAPI.Controllers
{
    public class SalesController : Controller
    {
        private readonly IRepositoryReadOnly<Customer> _customerRepository;
        private readonly IRepository<Invoice> _invoiceRepository;

        public SalesController(
            IRepositoryReadOnly<Customer> customerRepository,
            IRepository<Invoice> invoiceRepository)
        {
            _customerRepository = customerRepository;
            _invoiceRepository = invoiceRepository;
        }
        
        [Route("api/customer")]
        [HttpGet]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        [Route("api/customer/{id}")]
        [HttpGet]
        public IActionResult GetCustomerById(long id)
        {
            var customer = _customerRepository.FindById(id);
            if (customer == default(Customer))
            {
                return NotFound();
            }
            return new ObjectResult(customer);
        }

        [Route("api/invoice")]
        [HttpGet]
        public IEnumerable<Invoice> GetAllInvoices()
        {
            return _invoiceRepository.GetAll();
        }
        
        [HttpGet("api/invoice/{id}", Name = "GetInvoice")]
        public IActionResult GetInvoiceById(long id)
        {
            var invoice = _invoiceRepository.FindById(id);
            if (invoice == default(Invoice))
            {
                return NotFound();
            }
            return new ObjectResult(invoice);
        }

        [Route("api/invoice")]
        [HttpPost]
        public IActionResult CreateInvoice([FromBody] Invoice invoice)
        {
            if (invoice == default(Invoice))
            {
                return BadRequest();
            }

            _invoiceRepository.Add(invoice);

            return CreatedAtRoute(routeName: "GetInvoice", routeValues: new { id = invoice.Id }, value: invoice);
        }

        [Route("api/invoice/{id}")]
        [HttpPut]
        public IActionResult UpdateInvoice(long id, [FromBody] Invoice invoice)
        {
            if (invoice == default(Invoice) || invoice.Id != id)
            {
                return BadRequest();
            }

            var storedInvoice = _invoiceRepository.FindById(id);

            if (storedInvoice == default(Invoice))
            {
                return NotFound();
            }

            storedInvoice.Status = invoice.Status;

            _invoiceRepository.Update(storedInvoice);
            return new NoContentResult();
        }

        [Route("api/invoice/{id}")]
        [HttpDelete]
        public IActionResult DeleteInvoice(long id)
        {
            var invoice = _invoiceRepository.FindById(id);

            if (invoice == default(Invoice))
            {
                return NotFound();
            }

            _invoiceRepository.Remove(id);
            return new NoContentResult();
        }
    }
}
