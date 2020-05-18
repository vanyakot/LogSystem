using Log_system.Data.Model;
using Log_system.Services;
using System.Threading.Tasks;

namespace Log_system.Data.Receive
{
    public class DataController
    {
        private readonly IDataConverter _converter;
        private LogData data;
        public DataController(IDataConverter converter)
        {
            _converter = converter;
        }

        public async Task ReceiveInfo()
        {
           data = await _converter.WebResponseToObj<LogData>();
        }
    }
}
