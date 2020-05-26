using Log_system.Data.Model;
using Log_system.Infrastucture;
using Log_system.Model;
using Log_system.Services.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Log_system.Services
{
    public class DataHandler
    {
        private readonly IDataClient _dataClient;
        private readonly IErrorRepository _errorRepository;
        private readonly IWarningRepository _warningRepository;

        public DataHandler(IDataClient dataclient, IErrorRepository errorRepository, IWarningRepository warningRepository)
        {
            _dataClient = dataclient;
            _errorRepository = errorRepository;
            _warningRepository = warningRepository;

        }

        public async Task ReceiveInfo()
        {
            List<LogData> DataList = await _dataClient.GetResponse<List<LogData>>();
            if (DataList != null)
            {
                foreach (LogData data in DataList)
                {
                    if (data.HotelId != 0)
                    {
                        if (data.Error != null)
                        {
                            LogError error = new LogError()
                            {
                                HotelId = data.HotelId,
                                Error = data.Error,
                                AdditionalInfo = data.AdditionalInfo,
                                CreationDateTime = data.TimestampUtc
                            };
                            if (error.AdditionalInfo != null)
                            {
                                _errorRepository.AddErrorWithInfo(error);
                            } else
                            {
                                _errorRepository.AddError(error);
                            }

                        }
                        else if (data.Warning != null)
                        {
                            LogWarning warning = new LogWarning()
                            {
                                HotelId = data.HotelId,
                                Warning = data.Warning,
                                CreationDateTime = data.TimestampUtc
                            };
                            _warningRepository.AddWarning(warning);
                        }
                    }
                }
            }
        }
    }
}
