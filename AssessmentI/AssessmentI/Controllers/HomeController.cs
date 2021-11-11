using AssessmentI.Data.Entities;
using AssessmentI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AssessmentI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var values = new Dictionary<string, string>
            {
                { "IMEI", "354330030646882" },
                { "CompanyID", "10" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(" https://fleetsapapiqa.azurewebsites.net/api/GetIMEIDataServicesByIMEIAndCompany", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<ImeiCar>(responseString);

            return View(resultado);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
