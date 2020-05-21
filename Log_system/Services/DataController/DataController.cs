using Log_system.Data.Model;
using System.Threading.Tasks;

namespace Log_system.Services
{
    public class DataController
    {
        private readonly IDataClient _converter;

        public DataController(IDataClient converter)
        {
            _converter = converter;
        }

        public async Task ReceiveInfo()
        {
            LogData data = await _converter.GetResponse<LogData>();
        }
    }
}
