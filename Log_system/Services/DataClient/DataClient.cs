using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Log_system.Services
{
    public class DataClient : IDataClient
    {
        private readonly IHttpClient _client;

        public DataClient(IHttpClient client)
        {
            _client = client;
        }

        public async Task<T> GetResponse<T>()
        {
            string json;

            WebResponse response = await _client.GetHttpResponseMessage();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }
            response.Close();

            return JsonSerializer.Deserialize<T>(json);

        }
    }
}
