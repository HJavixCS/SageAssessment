using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HJCS.SageAssessment.ClientMVC.Models
{
    public class Invoice
    {
        public long Id { get; set; }

        public string Number { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public Status Status { get; set; }

        public long CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
