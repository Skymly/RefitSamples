using Refit;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RefitSamples.SharedApi
{
    public interface ITestApi
    {
        [Post("/api/Test/Add/{left}/{right}")]
        Task<int> Add(int left, int right);


        [Get("/api/Test/Repeat/{text}")]
        Task<string> Repeat(string text);

        [Get("/api/Test/Headers")]
        Task<Dictionary<string, string>> Header([Header("Head")]string str);

        [Headers("Headers:测试HeadersAttribute")]
        [Get("/api/Test/Headers")]
        Task<Dictionary<string, string>> Header();
    }
}
