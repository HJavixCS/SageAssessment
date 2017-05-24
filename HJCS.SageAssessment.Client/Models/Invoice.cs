using System;

namespace HJCS.SageAssessment.ClientMVC.Models
{
    public class Invoice
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public Status Status { get; set; }

        public long CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
