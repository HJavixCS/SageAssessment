using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HJCS.SageAssessment.ClientMVC.AppCode;
using Newtonsoft.Json;
using HJCS.SageAssessment.ClientMVC.Models;

namespace HJCS.SageAssessment.ClientMVC.Controllers
{
    public class HomeController : Controller
    {
        private string  WebApiUrl => "http://localhost:52379/api/";

        public IActionResult Index()
        {
            var actionURL = $"{WebApiUrl}/invoice";
            var response = DataRetriever.GetStringAsync(actionURL);
            var invoices = JsonConvert.DeserializeObject<List<Invoice>>(response);
            return View(invoices);
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
            return View();
        }
    }
}
