using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Terra
{
    public class TerraContext
    {
        public readonly HttpRequest Request;

        public readonly HttpResponse Response;
        
        public readonly RouteData RouteData;

        internal TerraContext(HttpRequest req, HttpResponse resp, RouteData data)
        {
            Request = req;
            Response = resp;
            RouteData = data;
        }
    }
}