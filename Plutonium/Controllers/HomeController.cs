using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plutonium.Interfaces;
using Plutonium.Models;
using Plutonium.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Plutonium.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProcessesService _processesService;
        private readonly IBatchFilesService _batchFilesService;

        public HomeController(ILogger<HomeController> logger, IProcessesService processesService, IBatchFilesService batchFilesService)
        {
            _logger = logger;
            _processesService = processesService;
            _batchFilesService = batchFilesService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BatchFiles()
        {
            List<BatchFileViewModel> bfvm = _batchFilesService.GetItems();
            return View(bfvm);
        }
        public IActionResult RunBatchFile(int BatchFileId)
        {
            RunBatchFileViewModel rbfvm = new RunBatchFileViewModel();
            try
            {
                _logger.LogInformation("Run Batch File : {0}", BatchFileId);
                rbfvm.FileName = _batchFilesService.GetFileName(BatchFileId);
                _logger.LogInformation("FileName : {0}", rbfvm.FileName);
                StringBuilder sb = _batchFilesService.RunBatchFile(BatchFileId);
                _logger.LogInformation("Output : {0}", sb.ToString());
                rbfvm.Output = sb.ToString();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return View(rbfvm);
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
