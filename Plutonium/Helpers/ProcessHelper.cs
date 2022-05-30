using Plutonium.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using mm = Plutonium.Models;

namespace Plutonium.Helpers
{
    public static class ProcessHelper
    {

        private static DBContext db = new DBContext();

        private static List<string> list = null;

        public static List<string> ProcessesListFromDatabase
        {
            get
            {
                if (list == null)
                {
                    list = db.Set<mm.Process>().Select(x => x.Name.ToUpper()).ToList<string>();
                }
                return list;
            }
        }

        public static List<string> GetMatchingProcesses()
        {

            var allProcesses = Process.GetProcesses().Select(x => x.ProcessName); //.ToList();
            List<string> matchProcesses = allProcesses.Where(x => ProcessesListFromDatabase.Contains(x.ToUpper())).ToList();
            return matchProcesses;
        }
        //         var processes = WebConfigurationManager.AppSettings["PROCESS_NAMES"];
        // list = processes.Split(';').Select(s => s.Trim()).ToList<string>();



        public static List<int> KillProcessesByName(string ProcessesName)
        {
            List<int> result = new List<int>();
            foreach (Process proc in Process.GetProcessesByName(ProcessesName))  // PROCESS_NAME
            {
                //    Response.Write(string.Format("{0}:{1}<BR>", proc.ProcessName, proc.Id));
                result.Add(proc.Id);
                proc.Kill();
            }
            return result;
        }

        public static bool IsRunning(string ProcessName)
        {
            bool result = false;
            try
            {
                if (Process.GetProcessesByName(ProcessName).Count() > 0) // PROCESS_NAME
                    result = true;
            }
            catch (Exception ex)
            {
                //  Response.Write(ex.Message);
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