using Log_system.Data.Model;
using Log_system.Services;
using System;
using System.Text.Json;

namespace Log_system.Data.Receive
{
    public class Handler
    {
        private IHttpClient _client;
        public Handler(IHttpClient client)
        {
            _client = client;
        }

        public void ReceiveInfo()
        {
            _client.SendGetRequest();
            if (_client._response != null || !_client._response.Equals(String.Empty))
            {
                LogData data = JsonSerializer.Deserialize<LogData>(_client._response);

            }
        }
    }
}
