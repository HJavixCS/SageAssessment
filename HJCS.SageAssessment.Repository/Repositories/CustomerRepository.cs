using HJCS.SageAssessment.Data;
using HJCS.SageAssessment.Domain.Data;
using HJCS.SageAssessment.Domain.Model;
using HJCS.SageAssessment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HJCS.SageAssessment.Repository.Repositories
{
    public class CustomerRepository : IRepositoryReadOnly<Customer>
    {
        private readonly SalesContext _context;

        public CustomerRepository(SalesContext context)
        {
            _context = context;

            if (_context.Customers.Count() == 0)
            {
                // TODO: Implement cache?
                PopulateCustomers();
            }
        }

        private void PopulateCustomers()
        {
            _context.Customers.Add(new Customer { FirstName = "Jonh", LastName = "Smith" });
            _context.Customers.Add(new Customer { FirstName = "Sara", LastName = "Jonhson" });
            _context.Customers.Add(new Customer { FirstName = "Bob", LastName = "Anderson" });
        }

        public Customer FindById(long id)
        {
            return _context.Customers.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }
    }
}
