using Log_system.Services;

namespace Log_system.Data.Receive
{
    public class Handler
    {
        private readonly IHttpClient _client;
        public Handler(IHttpClient client)
        {
            _client = client;
        }

        public void ReceiveInfo()
        {
      //          LogData data = JsonSerializer.Deserialize<LogData>();
        }
    }
}
