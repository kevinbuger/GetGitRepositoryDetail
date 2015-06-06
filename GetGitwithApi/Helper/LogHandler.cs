using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GetGitwithApi.Helper
{
    public class LogHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LogRequest(request);

            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                var response = task.Result;

                LogResponse(response);

                return response;
            });
        }

        private void LogRequest(HttpRequestMessage request)
        {
            (request.Content ?? new StringContent("")).ReadAsStringAsync().ContinueWith(x =>
            {
                LogSaver(string.Format("{4:yyyy-MM-dd HH:mm:ss} {5} {0} request [{1}]{2} - {3}", request.GetCorrelationId(), request.Method, request.RequestUri, x.Result, DateTime.Now, Username(request)));
            });
        }

        private void LogResponse(HttpResponseMessage response)
        {
            var request = response.RequestMessage;
            (response.Content ?? new StringContent("")).ReadAsStringAsync().ContinueWith(x =>
            {
                LogSaver(string.Format( "{3:yyyy-MM-dd HH:mm:ss} {4} {0} response [{1}] - {2}", request.GetCorrelationId(), response.StatusCode, x.Result, DateTime.Now, Username(request)));
            });
        }

        private string Username(HttpRequestMessage request)
        {
            var values = new List<string>().AsEnumerable();
            if (request.Headers.TryGetValues("my-custom-header-for-current-user", out values) == false) return "<anonymous>";

            return values.First();
        }

        public void LogSaver(string info)
        {
            Logger logger = new Logger();
            logger.Info = info;
            string json = JsonConvert.SerializeObject(logger);
            System.IO.File.WriteAllText(System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/LogFile/Logs-{0}.txt",DateTime.Now.ToString("yyyyMMddHHmmss"))), json);
        }
    }
}