using Microsoft.Extensions.DependencyInjection;
using Plutonium.Classes;
using Plutonium.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using mm = Plutonium.Models;

namespace Plutonium.Services
{
    public class ProcessesService : IProcessesService
    {

        private readonly IServiceScopeFactory scopeFactory;

        public ProcessesService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public List<string> GetProcesses()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DBContext>();
                return db.Set<mm.Process>().Select(x => x.Name.ToUpper()).ToList<string>();
                // when we exit the using block,
                // the IServiceScope will dispose itself 
                // and dispose all of the services that it resolved.
            }
        }


        public List<int> KillProcessesByName(string ProcessesName)
        {
            List<int> result = new List<int>();
            foreach (Process proc in Process.GetProcessesByName(ProcessesName))
            {
                result.Add(proc.Id);
                proc.Kill();
            }
            return result;
        }

        public List<string> GetMatchingProcesses()
        {
            var allProcesses = Process.GetProcesses().Select(x => x.ProcessName);
            List<string> matchProcesses = allProcesses.Where(x => GetProcesses().Contains(x.ToUpper())).ToList();
            return matchProcesses;
        }

        public bool IsRunning(string ProcessName)
        {
            bool result = false;
            if (Process.GetProcessesByName(ProcessName).Count() > 0)
            {
                result = true;
            }
            return result;
        }

        public static void ShutdownPC()
        {

            // ShutdownPC.bat
            Process myProcess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "shutdown";
            startInfo.Arguments = "/s /t 5";
            startInfo.UseShellExecute = true;
            startInfo.Verb = "runas";
            myProcess.StartInfo = startInfo;
            myProcess.Start();

            //Process myProcess = new Process();
            //myProcess.StartInfo.WorkingDirectory = Request.MapPath("~/bin");
            //myProcess.StartInfo.FileName = Request.MapPath("~/bin/ShutdownPC.bat");
            //myProcess.Start();

        }

    }
}
