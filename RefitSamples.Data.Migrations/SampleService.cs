using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RefitSamples.Data.Migrations
{
    internal class SampleService : IHostedService, IDisposable
    {
        private IServiceProvider serviceProvider;

        public SampleService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Dispose()
        {


        }

        public void Execute()
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var db = services.GetService<RefitSamplesDbContext>();
            Log.Information("HelloModels.Count = " + db.HelloModels.Count().ToString());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Execute();
           return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
