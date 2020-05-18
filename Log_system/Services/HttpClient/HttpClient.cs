using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace Log_system.Services
{
    public class HttpClient : IHttpClient
    {
        private readonly string _source;

        public HttpClient(IConfiguration configuration)
        {
            _source = configuration.GetSection("DataUrl").Value;
        }

        public async Task<WebResponse> GetHttpResponseMessage()
        {
            WebRequest request = WebRequest.Create(_source);
            return await request.GetResponseAsync();

        }

        
    }
}
