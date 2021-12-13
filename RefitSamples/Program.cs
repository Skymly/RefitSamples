using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog.Events;
using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

 

namespace RefitSamples
{
    public class Program
    {
        static readonly DateTime StartTime = DateTime.Now;
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Verbose()
#else
                .MinimumLevel.Verbose()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                   .Enrich.FromLogContext()
                   .WriteTo.Async(c => c.File(Path.Combine(AppContext.BaseDirectory, $"Logs/{StartTime:yyyyMMddHHmmssfff}/logs.txt")))
                   .WriteTo.Console()
                   .CreateLogger();
            Log.Information("App Started");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
