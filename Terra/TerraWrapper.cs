using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Terra
{
    internal class TerraWrapper
    {
        public string Verb { get; set; }
        public Func<TerraContext, Task> Handler { get; set; }
        public string Path { get; set; }

        public async Task Wrapper(HttpRequest req, HttpResponse res, RouteData data)
        {
            await Handler.Invoke(new TerraContext(req, res, data));
        }
    }
}