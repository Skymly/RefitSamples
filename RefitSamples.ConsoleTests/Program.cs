using RefitSamples.SharedApi;

using System;
using Refit;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Serilog.Events;
using Serilog;
using System.IO;
using Splat;
using Splat.Serilog;
namespace RefitSamples.ConsoleTests
{
    public class Program
    {
        static readonly DateTime StartTime = DateTime.Now;
        static async Task Main(string[] args)
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

            Locator.CurrentMutable.UseSerilogFullLogger();

            var client = new HttpClient(new HttpClientDiagnosticsHandler())
            {
                BaseAddress = new Uri("http://localhost:5000"),
            };
            ITestApi api = RestService.For<ITestApi>(client);
            var result = await api.Repeat("你好");
            Console.WriteLine(result);

            var result2 = await api.Header("HeaderTest");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result2));
            Console.ReadLine();
        }
    }
}
