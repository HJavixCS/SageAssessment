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
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
