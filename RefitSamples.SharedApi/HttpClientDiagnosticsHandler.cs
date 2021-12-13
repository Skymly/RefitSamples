using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Splat;
using System.Net;

namespace RefitSamples.SharedApi
{
    public class HttpClientDiagnosticsHandler : DelegatingHandler, IEnableLogger
    {
        public HttpClientDiagnosticsHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        public HttpClientDiagnosticsHandler()
        {
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //request.Version = HttpVersion.Version11;
            var totalElapsedTime = Stopwatch.StartNew();
            this.Log().Debug(string.Format("Request: {0}", request));
            if (request.Content != null)
            {
                var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                this.Log().Debug(string.Format("Request Content: {0}", content));
            }
            var responseElapsedTime = Stopwatch.StartNew();
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            this.Log().Debug(string.Format("Response: {0}", response));
            if (response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                this.Log().Debug(string.Format("Response Content: {0}", content));
            }
            responseElapsedTime.Stop();
            this.Log().Debug(string.Format("Response elapsed time: {0} ms", responseElapsedTime.ElapsedMilliseconds));

            totalElapsedTime.Stop();
            this.Log().Debug(string.Format("Total elapsed time: {0} ms", totalElapsedTime.ElapsedMilliseconds));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
