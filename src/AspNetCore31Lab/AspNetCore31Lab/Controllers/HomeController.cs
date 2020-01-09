using System.Diagnostics;
using AspNetCore31Lab.Models;
using AspNetCore31Lab.Protocol.Logic;
using Autofac.Features.AttributeFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCore31Lab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IDataService dataService;

        public HomeController(ILogger<HomeController> logger, [KeyFilter("default")] IDataService dataService)
        {
            this.logger = logger;
            this.dataService = dataService;
        }

        public IActionResult Index()
        {
            var data = this.dataService.GetName();

            this.ViewBag.Data = data;

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}