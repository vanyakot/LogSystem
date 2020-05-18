using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Log_system.Services.DataConverter
{
    public class DataConverter : IDataConverter
    {
        private readonly IHttpClient _client;

        private WebResponse response;

        public DataConverter(IHttpClient client)
        {
            _client = client;
        }

        public T WebResponseToObj<T>()
        {
            response = _client.GetHttpResponseMessage();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    reader.ReadToEnd();
                }
            }
            response.Close();

        }
    }
}
