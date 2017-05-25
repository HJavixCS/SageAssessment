using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HJCS.SageAssessment.ClientMVC.AppCode;
using Newtonsoft.Json;
using HJCS.SageAssessment.ClientMVC.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using System;
using HJCS.SageAssessment.ClientMVC.Models.ViewModels;

namespace HJCS.SageAssessment.ClientMVC.Controllers
{
    public class HomeController : Controller
    {
        private string  WebApiInvoiceUri => "http://localhost:52379/api/invoice";
        private string WebApiCustomerUri => "http://localhost:52379/api/customer";

        public async Task<IActionResult> Index()
        {
            var response = await HttpClientHelper.GetStringAsync(WebApiInvoiceUri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                var invoices = JsonConvert.DeserializeObject<List<Invoice>>(responseString);
                return View(invoices);
            }
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new InvoiceCreation() { Customers = await GetCustomersAsync() };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Date, Description, Amount, CustomerId")] InvoiceCreation invoiceCreation)
        {
            if (ModelState.IsValid)
            {
                var invoice = GetInvoiceFromModel(invoiceCreation);
                var data = JsonConvert.SerializeObject(invoice);
                var response = await HttpClientHelper.PostAsync(WebApiInvoiceUri, data);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            invoiceCreation.Customers = await GetCustomersAsync();
            return View(invoiceCreation);
        }

        public async Task<IActionResult> Edit(long id)
        {
            if (id == default(long))
            {
                return NotFound();
            }
            var invoice = await FindInvoiceAsync(id);
            var invoiceEdition = MapInvoiceFromEntity(invoice);
            return View(invoiceEdition);
        }

        private InvoiceEdition MapInvoiceFromEntity(Invoice invoice)
        {
            return new InvoiceEdition
            {
                Id = invoice.Id,
                Date = invoice.Date,
                Description = invoice.Description,
                Number = invoice.Number,
                Amount = invoice.Amount,
                Status = invoice.Status,
                Customer = invoice.Customer.FullName
            };
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, Invoice invoice)
        {
            if (id == default(long))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var data = JsonConvert.SerializeObject(invoice);
                var actionUri = $"{WebApiInvoiceUri}/{id}";
                var response = await HttpClientHelper.PutAsync(actionUri, data);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(invoice);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Sage Assessment";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Héctor Javier Castillo Suazo";

            return View();
        }

        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();

            ViewData["statusCode"] = HttpContext.Response.StatusCode;
            ViewData["message"] = exception.Error.Message;
            ViewData["stackTrace"] = exception.Error.StackTrace;

            return View();
        }

        private async Task<Invoice> FindInvoiceAsync(long id)
        {
            var invoice = new Invoice();
            var actionUri = $"{WebApiInvoiceUri}/{id}";
            var response = await HttpClientHelper.GetStringAsync(actionUri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                invoice = JsonConvert.DeserializeObject<Invoice>(responseString);
            }

            return invoice;
        }

        private async Task<List<Customer>> GetCustomersAsync()
        {
            var customers = new List<Customer>();
            var response = await HttpClientHelper.GetStringAsync(WebApiCustomerUri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                customers = JsonConvert.DeserializeObject<List<Customer>>(responseString);
            }

            return customers;
        }

        private Invoice GetInvoiceFromModel(InvoiceCreation invoiceCreation)
        {
            return new Invoice
            {
                Date = invoiceCreation.Date,
                Description = invoiceCreation.Description,
                Amount = invoiceCreation.Amount,
                CustomerId = invoiceCreation.CustomerId
            };
        }
    }
}
