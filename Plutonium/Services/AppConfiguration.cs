using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plutonium.Services
{
    public class AppConfiguration
    {
        public AppConfiguration() { }
        public int BackgroundServiceDelay { get; set; }
        public string AppName { get; set; }        
        public string AppVersion { get; set; }
        
    }
}
