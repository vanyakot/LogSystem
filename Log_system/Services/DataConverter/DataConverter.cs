using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Log_system.Services
{
    public class DataConverter : IDataConverter
    {
        private readonly IHttpClient _client;

        private WebResponse response;

        private string json;


        public DataConverter(IHttpClient client)
        {
            _client = client;
        }

        public async Task<T> WebResponseToObj<T>()
        {
            response = await _client.GetHttpResponseMessage();
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
