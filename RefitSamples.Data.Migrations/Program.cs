using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog.Events;
using Serilog;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Extensions.Logging;

namespace RefitSamples.Data.Migrations
{

    //dotnet ef migrations add InitialCreate -p .\RefitSamples.Data -s RefitSamples.Data.Migrations

    internal class Program
    {
        static readonly DateTime StartTime = DateTime.Now;
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Verbose()
#else
                .MinimumLevel.Verbose()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                 .Enrich.FromLogContext()
                 .WriteTo.Async(c => c.File($"Logs/{StartTime:yyyyMMddHHmmssfff}/logs.txt"))
                 .WriteTo.Console()
                 .CreateLogger();
            Log.Information("App Started");
            CreateHostBuilder(args).Build().Run();
            Log.Information("Done");
        }


        public static IHostBuilder CreateHostBuilder(string [] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
            {
                services.AddHostedService<SampleService>();
                services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
                services.AddDbContext<RefitSamplesDbContext>(options =>
                {
                    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection") ,x=>x.MigrationsAssembly("RefitSamples.Data"));
                });
            });
        }

    }
}
