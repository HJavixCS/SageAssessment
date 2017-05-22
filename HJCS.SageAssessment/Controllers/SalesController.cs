using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HJCS.SageAssessment.Domain.Model;
using HJCS.SageAssessment.Domain.Repositories;

namespace HJCS.SageAssessment.WebAPI.Controllers
{
    public class SalesController : Controller
    {
        private readonly IRepositoryReadOnly<Customer> _customerRepository;

        public SalesController(IRepositoryReadOnly<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
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
    }
}
