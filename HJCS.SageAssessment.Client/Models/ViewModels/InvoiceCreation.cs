using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HJCS.SageAssessment.ClientMVC.Models.ViewModels
{
    public class InvoiceCreation
    {
        public InvoiceCreation()
        {
            Date = DateTime.Now;
        }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public long CustomerId { get; set; }

        public List<Customer> Customers { get; set; }

        private List<Customer> GetCustomers()
        {
            return Customers ?? new List<Customer> { };
        }

        public List<SelectListItem> CustomerList
        {
            get
            {
                return GetCustomers().OrderBy(c => c.FullName).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.FullName }).ToList();
            }
        }
    }
}
