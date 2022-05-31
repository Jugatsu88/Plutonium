using Microsoft.Extensions.Hosting;
using Plutonium.Helpers;
using Plutonium.Models;
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
        private readonly Subject<ProcessModel> _subject = new Subject<ProcessModel>();
        private readonly Random _random = new Random();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
              //  var x = ProcessHelper.GetMatchingProcesses().GroupBy(x=> x).ToArray();
                var result = ProcessHelper.GetMatchingProcesses().GroupBy(n => n)
                    .Select(c => new { Key = c.Key, Count = c.Count() });


                _subject.OnNext(new ProcessModel { LastUpdatedDate = DateTime.Now, ProcessName = result.FirstOrDefault().Key, ProcessCount = result.FirstOrDefault().Count });  //_random.Next(0, 40) });

                await Task.Delay(1000);
            }
        }

        public IObservable<ProcessModel> StreamProcesses()
        {
            return _subject;
        }
    }
}
