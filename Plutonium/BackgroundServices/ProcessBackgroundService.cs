using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Plutonium.Helpers;
using Plutonium.Models;
using Plutonium.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace Plutonium.BackgroundServices
{
    public class ProcessBackgroundService : BackgroundService
    {

        private readonly IOptions<AppConfiguration> _appConfiguration;
        private readonly ILogger<ProcessBackgroundService> _logger;

        private readonly Subject<List<ProcessModel>> _subject = new Subject<List<ProcessModel>>();
        private readonly Random _random = new Random();

        public ProcessBackgroundService(ILogger<ProcessBackgroundService> logger, IOptions<AppConfiguration> appConfiguration)
        {
            _logger = logger;
            _appConfiguration = appConfiguration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                List<ProcessModel> pvm = new List<ProcessModel>();
                var matchingProcesses = ProcessHelper.GetMatchingProcesses();
                pvm = matchingProcesses.GroupBy(x => x)
                  .Select(o => new ProcessModel { ProcessName = o.Key, ProcessCount = o.Count(), LastUpdatedDate = DateTime.Now })
                  .ToList<ProcessModel>();

                _subject.OnNext(pvm);  //_random.Next(0, 40) });

                await Task.Delay(_appConfiguration.Value.BackgroundServiceDelay * 1000); // Config delay in seconds
            }
        }

        public IObservable<List<ProcessModel>> StreamProcesses()
        {
            return _subject;
        }
    }
}
