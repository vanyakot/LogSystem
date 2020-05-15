using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log_system.Services.Repositories
{
    public class LogRepository:ILogRepository
    {
        private string connectionString;
        public LogRepository(IConfiguration config)
        {
            connectionString = config["LogData"];
        }

        public void CreateTable()
        {

        }
    }
}
