using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Log_system.Services
{
    public class HttpClient:IHttpClient
    {
        private string _source;
        public string _response { get; set; }

        public HttpClient(IConfiguration configuration)
        {
            _source = configuration.GetSection("DataUrl").Value;
        }
        public async Task SendGetRequest()
        {
            WebRequest request = WebRequest.Create(_source);
            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    _response = reader.ReadToEnd();
                }
            }
            response.Close();
        }
    }
}
