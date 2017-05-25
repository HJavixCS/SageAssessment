using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HJCS.SageAssessment.ClientMVC.AppCode;
using Newtonsoft.Json;
using HJCS.SageAssessment.ClientMVC.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using System;

namespace HJCS.SageAssessment.ClientMVC.Controllers
{
    public class HomeController : Controller
    {
        private string  WebApiInvoiceUri => "http://localhost:52379/api/invoice";

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Date, Description, Amount, CustomerId")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                var data = JsonConvert.SerializeObject(invoice);
                var response = await HttpClientHelper.PostAsync(WebApiInvoiceUri, data);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(invoice);
        }

        public async Task<IActionResult> Edit(long id)
        {
            if (id == default(long))
            {
                return NotFound();
            }
            var invoice = await FindInvoiceAsync(id);
            return View(invoice);
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
    }
}
