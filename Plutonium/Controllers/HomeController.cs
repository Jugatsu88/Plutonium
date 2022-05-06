using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plutonium.Helpers;
using Plutonium.Models;
using Plutonium.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Plutonium.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            ////TODO : add Signal R
            //var matchingProcesses = ProcessHelper.GetMatchingProcesses();
            //var ProcessCounts = matchingProcesses.GroupBy(x => x)
            //    .Select (g => string.Format("{0} ({1})", g.Key, g.Count()))
            //    .ToList<string>();
            //return View(ProcessCounts);

            List<ProcessViewModel> pvm = new List<ProcessViewModel>();
            var matchingProcesses = ProcessHelper.GetMatchingProcesses();
            pvm = matchingProcesses.GroupBy(x => x)
              .Select(o => new ProcessViewModel { Name = o.Key, Count = o.Count() })
              .ToList<ProcessViewModel>();

            return View(pvm);

        }
        public IActionResult KillProcess(string ProcessName)
        {
            ProcessHelper.KillProcessesByName(ProcessName);
            return RedirectToAction("Index");

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
