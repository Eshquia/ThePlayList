using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using ThePlayList.Work.Checker;

namespace ThePlayList.Work
{
    public class RuntimeCheckerHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRuntimeChecker _runtimeChecker;
        private Timer _timer;

        public RuntimeCheckerHostedService(IServiceProvider serviceProvider, IRuntimeChecker runtimeChecker)
        {
            _serviceProvider = serviceProvider;
            _runtimeChecker = runtimeChecker;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _runtimeChecker.CheckRuntime();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}