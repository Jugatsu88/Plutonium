using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plutonium.Interfaces
{
    public interface IProcessesService
    {
        public List<string> GetProcesses();
        public List<int> KillProcessesByName(string ProcessesName);
        public List<string> GetMatchingProcesses();
        public bool IsRunning(string ProcessName);
    }
}
