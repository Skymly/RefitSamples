using Newtonsoft.Json;

using NUnit.Framework;

using RefitSamples.Models;
using RefitSamples.SharedApi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefitSamples.SharedApiTests
{
    [TestOf(typeof(IWeatherApi))]
    public class WeatherApiTests : ApiTestBase<IWeatherApi>
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        [Test]
        public async Task Get()
        {
            var result = await Api.Get();
            if (result is IEnumerable<WeatherForecast> items)
            {
                Assert.Pass(JsonConvert.SerializeObject(items, Formatting.Indented));
            }
            else
            {
                Assert.Fail();
            }


        }

    }
}
