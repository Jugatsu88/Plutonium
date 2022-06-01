using Microsoft.AspNetCore.SignalR;
using Plutonium.BackgroundServices;
using Plutonium.Extensions;
using Plutonium.Helpers;
using Plutonium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Plutonium.Hubs
{
    public class ProcessHub : Hub
    {
        private readonly ProcessBackgroundService _weatherBackgroundService;
        public ProcessHub(ProcessBackgroundService weatherBackgroundService)
        {
            _weatherBackgroundService = weatherBackgroundService;
        }

        public ChannelReader<List<ProcessModel>> StreamProcesses()
        {
            return _weatherBackgroundService.StreamProcesses().AsChannelReader(10);
        }

        public void KillProcess(string processesName)
        {
            ProcessHelper.KillProcessesByName(processesName);
        }


    }
}
