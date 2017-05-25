using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HJCS.SageAssessment.ClientMVC.Models.ViewModels
{
    public class InvoiceEdition
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public Status Status { get; set; }

        public long CustomerId { get; set; }

        public string Customer { get; set; }

        private List<Status> GetStatuses()
        {
            return Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
        }

        public List<SelectListItem> StatusList
        {
            get
            {
                return GetStatuses().OrderBy(c => c).Select(i => new SelectListItem { Value = i.ToString(), Text = i.ToString() }).ToList();
            }
        }
    }
}
