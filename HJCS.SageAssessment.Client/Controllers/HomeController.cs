﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HJCS.SageAssessment.ClientMVC.AppCode;
using Newtonsoft.Json;
using HJCS.SageAssessment.ClientMVC.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using HJCS.SageAssessment.ClientMVC.Models.ViewModels;
using Microsoft.Extensions.Configuration;

namespace HJCS.SageAssessment.ClientMVC.Controllers
{
    public class HomeController : Controller
    {
        private string  _webApiInvoiceUri;
        private string _webApiCustomerUri;
        IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiInvoiceUri = _configuration["WebApi:InvoiceUri"];
            _webApiCustomerUri = _configuration["WebApi:CustomerUri"];
        }
        public async Task<IActionResult> Index()
        {
            var response = await HttpClientHelper.GetStringAsync(_webApiInvoiceUri);

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
                var invoice = MapInvoiceFromModel(invoiceCreation);
                var data = JsonConvert.SerializeObject(invoice);
                var response = await HttpClientHelper.PostAsync(_webApiInvoiceUri, data);

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
            return View(MapInvoiceFromEntity(invoice));
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
                var actionUri = $"{_webApiInvoiceUri}/{id}";
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
            ViewData["Message"] = _configuration["AboutMessage"];

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = _configuration["ContactMessage"];

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
            var actionUri = $"{_webApiInvoiceUri}/{id}";
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
            var response = await HttpClientHelper.GetStringAsync(_webApiCustomerUri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                customers = JsonConvert.DeserializeObject<List<Customer>>(responseString);
            }

            return customers;
        }

        private Invoice MapInvoiceFromModel(InvoiceCreation invoiceCreation)
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
