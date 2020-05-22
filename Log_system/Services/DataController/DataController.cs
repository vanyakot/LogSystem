using Log_system.Data.Model;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace Log_system.Services
{
    public class DataController
    {
        private readonly IDataClient _dataClient;
        private readonly ILogRepository _repository;

        public DataController(IDataClient converter, ILogRepository repository)
        {
            _dataClient = converter;
            _repository = repository;
        }

        public async Task ReceiveInfo()
        {
            LogData data = await _dataClient.GetResponse<LogData>();
            if (data != null)
            {
                data.TimeRecieve = DateTime.Now;
                if (data.Error != null)
                {
                    _repository.AddError(data);

                }
                else if (data.Warning != null)
                {
                    _repository.AddWarning(data);
                }
            }
        }
    }
}
