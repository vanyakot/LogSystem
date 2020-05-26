using Log_system.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log_system.Services.Repositories
{
    public interface IWarningRepository
    {
        void AddWarning(LogWarning data);
        List<LogWarning> FindWarningsByDate(DateTime StartDate, DateTime EndDate);
        List<LogWarning> FindWarningsByIdAndDate(int HotelId, DateTime StartDate, DateTime EndDate);
    }
}
