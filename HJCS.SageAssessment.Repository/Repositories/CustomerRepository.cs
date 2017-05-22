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
            _context.Customers.Add(new Customer { FirstName = "Jonh", LastName = "Smith", CustomerNumber = "1001" });
            _context.Customers.Add(new Customer { FirstName = "Sara", LastName = "Jonhson", CustomerNumber = "1002" });
            _context.Customers.Add(new Customer { FirstName = "Bob", LastName = "Anderson", CustomerNumber = "1003" });
            _context.SaveChanges();
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
