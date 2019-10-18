using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Terra.Tests
{
    public class TerraTest
    {
        [Fact]
        public void StartsWithoutError()
        {
            var terra = new TerraServer();
            terra.ListenAsync(8080);
        }

        [Fact]
        public async Task CheckGet()
        {
            var terra = new TerraServer();
            terra.Get("/test", async ctx => { await ctx.Response.WriteAsync("Test"); });
            await terra.ListenAsync(8081);
            var client = new WebClient();
            var resp = await client.DownloadStringTaskAsync("http://localhost:8081/test");
            Assert.Equal("Test", resp);
        }
    }
}