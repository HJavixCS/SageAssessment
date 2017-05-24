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
            //var actionURL = $"{WebApiInvoiceUrl}/{id}";
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
        public async Task<IActionResult> Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                var data = JsonConvert.SerializeObject(invoice);
                var response = await HttpClientHelper.PostAsync(WebApiInvoiceUri, data);

                if (response.IsSuccessStatusCode)
                {
                    return View(invoice);
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
