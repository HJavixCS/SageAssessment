using System;
using System.ComponentModel.DataAnnotations;

namespace HJCS.SageAssessment.ClientMVC.Models
{
    public class Invoice
    {
        public long Id { get; set; }

        public string Number { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
        
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        public Status Status { get; set; }

        public long CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
