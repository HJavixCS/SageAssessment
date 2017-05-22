using Microsoft.EntityFrameworkCore;
using HJCS.SageAssessment.Domain.Model;

namespace HJCS.SageAssessment.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options) { }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Customer> Customers { get; set; }
    }
}
