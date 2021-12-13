using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace RefitSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet("Repeat/{text}")]
        public ActionResult<string> Repeat(string text)
        {
            return "人类的本质就是复读机" + Environment.NewLine + string.Join(",", Enumerable.Repeat(text, 5));
        }

        [HttpPost("Add/{left}/{right}")]
        public ActionResult<int> Add(int left,int right)
        {
            return left + right;
        }


        [HttpGet]
        [Route("Delay/{milliseconds}")]
        public async Task<string> DelayTest(int milliseconds = 5000)
        {
            var d = TimeSpan.FromMilliseconds(milliseconds);
            var t1 = DateTime.Now;
            await Task.Delay(d);
            var t2 = DateTime.Now;
            return $"等待了{d.TotalSeconds:F3}秒后返回,[收到请求={t1:yyyy/MM/dd HH:mm:ss.fff}],[应答时间{t2:yyyy/MM/dd HH:mm:ss.fff}]";
        }

        [HttpGet]
        [Route("TimeOut")]
        public IActionResult TimeOutTest()
        {
            return StatusCode(StatusCodes.Status408RequestTimeout,"超时了");
        }


        [HttpGet("Headers")]
        public Dictionary<string, string> Headers()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (var item in Request.Headers)
            {
                dict.Add(item.Key, item.Value);
            }
            return dict;
        }


    }
}
