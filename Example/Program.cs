using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Terra;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run().GetAwaiter().GetResult();
        }

        public async Task Run()
        {
            var terra = new TerraServer();
            terra.Get("/", async ctx => { await ctx.Response.WriteAsync("Get received"); });
            terra.Post("/post", async ctx =>
            {
                var param = ctx.Request.Form["Hello"];
                await ctx.Response.WriteAsync(param);
            });

            await terra.ListenAsync(8080);
            await Task.Delay(-1);
        }
    }
}