using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Terra
{
    public class TerraInternal
    {
        private IWebHost _host;

        private List<TerraWrapper> _handlers = new List<TerraWrapper>();
        private List<Func<HttpContext,Func<Task>, Task>> _middlewares = new List<Func<HttpContext,Func<Task>, Task>>();
        
        
        internal void CreateWebhost(string url)
        {
            _host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(x => { x.AddRouting(); })
                .Configure(app =>
                {
                    BuildMiddlewares(app);
                    app.UseRouter(BuildRoutes);
                })
                .UseUrls(url)
                .Build();
        }

        private void BuildRoutes(IRouteBuilder builder)
        {
            foreach (var handler in _handlers)
            {
                builder.MapVerb(handler.Verb, handler.Path, handler.Wrapper);
            }
        }

        private void BuildMiddlewares(IApplicationBuilder builder)
        {
            foreach (var middleware in _middlewares)
            {
                builder.Use(middleware);
            }
        }

        internal async Task StartAsync() => await _host.StartAsync();

        internal void RegisterRoute(TerraWrapper wrapper)
        {
            _handlers.Add(wrapper);
        }

        internal void RegisterMiddleware(Func<HttpContext,Func<Task>, Task> middleware)
        {
            _middlewares.Add(middleware);
        }
        
    }
}