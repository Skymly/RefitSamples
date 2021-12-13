using NUnit.Framework;
using RefitSamples.SharedApiTests;
using Refit;
using System.Net.Http;
using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RefitSamples.Models;
using System.Reactive.Linq;

namespace RefitSamples.SharedApi.Tests
{
    [TestOf(typeof(IAccountApi))]
    public class AccountApiTests : ApiTestBase<IAccountApi>
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        [TestCase("Abc.001@qq.com", "Abc.001")]
        [Test]
        public void TokenTest(string email, string passwrod)
        {
            var input = new UserLoginInput(email, passwrod);
            Api.CreateToken(input)
               .Timeout(TimeSpan.FromSeconds(5))
               .Retry(3)
               .Do(OnNext, OnError)
               .Wait();
        }

        [Ignore("测试已经通过")]
        [TestCase("Abc.002@qq.com", "Abc.002")]
        [TestCase("Abc.003@qq.com", "Abc.003", "Abc.003")]
        [Test]
        public void RegisterTest(string email, string passwrod, string username = null)
        {
            var input = new UserRegistrationInput(email, passwrod, username);
            Api.Register(input)
               .Timeout(TimeSpan.FromSeconds(5))
               .Retry(3)
               .Do(OnNext, OnError)
               .Wait();
        }
    }
}