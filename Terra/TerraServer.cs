using System;
using System.Threading.Tasks;

namespace Terra
{
    public class TerraServer
    {
        private TerraInternal _internal = new TerraInternal();
        
        
        public void Get(string path, Func<TerraContext, Task> handler)
        {
            _internal.RegisterRoute(new TerraWrapper{Handler = handler, Path = path, Verb = "GET"});
        }

        public void Post(string path, Func<TerraContext, Task> handler)
        {
            _internal.RegisterRoute(new TerraWrapper{Handler = handler, Path = path, Verb = "POST"});
        }

        public void Delete(string path, Func<TerraContext, Task> handler)
        {
            _internal.RegisterRoute(new TerraWrapper{Handler = handler, Path = path, Verb = "DELETE"});
        }

        public void Put(string path, Func<TerraContext, Task> handler)
        {
            _internal.RegisterRoute(new TerraWrapper{Handler = handler, Path = path, Verb = "PUT"});
        }

        public async Task ListenAsync(int port, string host = "127.0.0.1")
        {
            var url = $"http://{host}:{port.ToString()}";
            _internal.CreateWebhost(url);
            await _internal.StartAsync();
        }
    }
}