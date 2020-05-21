using Log_system.Data.Model;
using System;

namespace Log_system.Services
{
    public interface ILogRepository
    {
        void AddError(LogData data, DateTime date);
        void AddWarning(LogData data, DateTime date);
    }
}
