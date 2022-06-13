using Microsoft.Extensions.DependencyInjection;
using Plutonium.Classes;
using Plutonium.Interfaces;
using Plutonium.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mm = Plutonium.Models;

namespace Plutonium.Services
{
    public class BatchFilesService : IBatchFilesService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public BatchFilesService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }
        public List<BatchFileViewModel> GetItems()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DBContext>();
                return db.Set<mm.BatchFile>().Select(x => new BatchFileViewModel { FileName = x.FileName, Id = x.Id }).ToList<BatchFileViewModel>();
            }
        }

        public string GetFileName(int BatchFileId)
        {
            string result = string.Empty;
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DBContext>();
                mm.BatchFile item = db.Set<mm.BatchFile>().Find(BatchFileId);
                if (item != null)
                    result = item.FileName;
            }
            return result;
        }
        public StringBuilder RunBatchFile(int BatchFileId)
        {
            StringBuilder sb = new StringBuilder();
            using (var scope = scopeFactory.CreateScope())
            {

                var db = scope.ServiceProvider.GetRequiredService<DBContext>();
                mm.BatchFile item = db.Set<mm.BatchFile>().Find(BatchFileId);

                if (item == null)
                    return new StringBuilder(string.Format("Batch File not found. BatchFileId : {0}", BatchFileId));

                Process p = new Process
                {
                    StartInfo = {
                        WorkingDirectory = @"c:\",
                        FileName = "cmd.exe",
                        Arguments = "/c " + item.Contents,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        UseShellExecute = false

                    }
                };
                p.OutputDataReceived += (sender, args) => sb.AppendLine(args.Data);
                //p.ErrorDataReceived += (sender, args) => sb.AppendLine("ERROR >> " + args.Data);
                p.Start();
                //string stdoutx = p.StandardOutput.ReadToEnd();
                string stderrx = p.StandardError.ReadToEnd();
                p.BeginOutputReadLine();
              //  p.BeginErrorReadLine();
                p.WaitForExit();

                if(!string.IsNullOrEmpty(stderrx))
                    sb.AppendLine("ERROR >> " + stderrx);


                sb.AppendLine(string.Format("ExitCode : {0}", p.ExitCode));

                return sb;

            }
        }

    }
}
