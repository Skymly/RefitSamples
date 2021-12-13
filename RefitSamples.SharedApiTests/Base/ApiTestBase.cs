using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Refit;
using RefitSamples.SharedApi.Tests;
using RefitSamples.SharedApi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RefitSamples.SharedApiTests
{
    public  class ApiTestBase<T> :TestClassBase
    {
       public  T Api { get; set; }

        public override void SetUp()
        {
            var client = new HttpClient(new HttpClientDiagnosticsHandler())
            {
                BaseAddress = new Uri("http://localhost:5000/"),
            };
            //var client = new HttpClient()
            //{
            //    BaseAddress = new Uri("http://localhost:5000/"),
            //};
            var settings = new RefitSettings(new NewtonsoftJsonContentSerializer(new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,//Null值不序列化
                //ContractResolver = new CamelCasePropertyNamesContractResolver(),
                // Converters = { new StringEnumConverter() }
            }));
            Api = RestService.For<T>(client, settings);
        }


    }
}
