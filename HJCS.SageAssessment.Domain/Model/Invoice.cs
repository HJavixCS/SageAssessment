﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HJCS.SageAssessment.Domain.Model
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Number => Id.ToString();

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public Status Status { get; set; }

        public long CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
