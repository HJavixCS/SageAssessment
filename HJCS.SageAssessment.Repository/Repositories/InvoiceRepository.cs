using HJCS.SageAssessment.Data;
using HJCS.SageAssessment.Domain.Model;
using HJCS.SageAssessment.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HJCS.SageAssessment.Repository.Repositories
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        private readonly SalesContext _context;

        public InvoiceRepository(SalesContext context)
        {
            _context = context;
        }

        public void Add(Invoice item)
        {
            item.Status = Status.Pending;
            _context.Invoices.Add(item);
            _context.SaveChanges();
        }

        public Invoice FindById(long id)
        {
            return _context.Invoices.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Invoice> GetAll()
        {
            return _context.Invoices.Include(t => t.Customer).ToList();
        }

        public void Remove(long id)
        {
            var item = _context.Invoices.First(t => t.Id == id);
            _context.Invoices.Remove(item);
            _context.SaveChanges();
        }

        public void Update(Invoice item)
        {
            _context.Invoices.Update(item);
            _context.SaveChanges();
        }
    }
}
