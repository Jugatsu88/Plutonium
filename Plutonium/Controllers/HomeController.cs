using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plutonium.Interfaces;
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
        private readonly IProcessesService _processesService;

        public HomeController(ILogger<HomeController> logger, IProcessesService processesService)
        {
            _logger = logger;
            _processesService = processesService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult KillProcess(string ProcessName)
        {

            try
            {
                _logger.LogInformation("Kill Process : {0}", ProcessName);
                _processesService.KillProcessesByName(ProcessName);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

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
