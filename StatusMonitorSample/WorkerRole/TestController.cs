using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WorkerRole
{
    public class TestController : ApiController
    {
        public HttpResponseMessage Get()
        {
            (new WebClient()).DownloadString("https://google.com");
            (new WebClient()).DownloadString("https://bing.com");

            return new HttpResponseMessage()
            {
                Content = new StringContent("Worker Role: Refresh this page to make some calls to external sites...")
            };
        }
    }
}