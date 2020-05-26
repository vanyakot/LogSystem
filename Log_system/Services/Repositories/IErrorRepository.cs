using Log_system.Data.Model;
using System;
using System.Collections.Generic;

namespace Log_system.Services.Repositories
{
    public interface IErrorRepository
    {
        void AddError(LogError data);
        void AddErrorWithInfo(LogError data);
        List<LogError> FindErrorsByDate(DateTime StartDate, DateTime EndDate);
        List<LogError> FindErrorsByIdAndDate(int HotelId, DateTime StartDate, DateTime EndDate);
    }
}
