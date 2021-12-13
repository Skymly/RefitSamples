using Newtonsoft.Json.Serialization;

using Newtonsoft.Json;
using NUnit.Framework;

using Refit;

using RefitSamples.Models;
using RefitSamples.SharedApi;
using RefitSamples.SharedApi.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace RefitSamples.SharedApiTests
{
    [TestOf(typeof(IAppUserApi))]
    public class AppUserApiTests : ApiTestBase<IAppUserApi>
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        [Test]
        public void GetRoles()
        {
            Api.Get()
               .Timeout(TimeSpan.FromSeconds(5))
               .Retry(3)
               .Do(OnNext, OnError)
               .Wait();
        }
    }
}
