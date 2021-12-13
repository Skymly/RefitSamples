using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using Splat;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Events;
using Serilog;
using Splat.Serilog;

namespace RefitSamples.SharedApiTests
{
    [TestFixture]
    public abstract class TestClassBase : IEnableLogger
    {
        static readonly DateTime StartTime = DateTime.Now;
        static TestClassBase()
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
               .WriteTo.Debug()
               .WriteTo.Console()
               .CreateLogger();
            Locator.CurrentMutable.UseSerilogFullLogger();
            Log.Information("App Started");

        }

        public virtual void SetUp()
        {
        }
        public virtual void OnNext<T>(T t)
        {
            var message = t is string or int or long or double or float or byte or DateTime or Guid ?
                             t.ToString()
                           : JsonConvert.SerializeObject(t, Formatting.Indented);
            var lines = message.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Console.WriteLine($"行数[{lines.Length}] 字符串长度[{message.Length}]");
            this.Log().Info(message);
        }

        public virtual void OnError<T>(T t) where T : Exception
        {
            this.Log().Error(t);
            Assert.Fail(t.ToString());
        }
    }
}
