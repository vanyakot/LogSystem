using Log_system.Data.Model;
using System;
using System.Collections.Generic;

namespace Log_system.Services
{
    public interface ILogRepository
    {
        void AddError(LogData data);
        void AddWarning(LogData data);
        List<LogData> FindErrorsById(int HotelId);
        List<LogData> FindErrorsByDate(DateTime StartDate, DateTime EndDate);
        List<LogData> FindErrorsByIdAndDate(int HotelId, DateTime StartDate, DateTime EndDate);
    }
}
