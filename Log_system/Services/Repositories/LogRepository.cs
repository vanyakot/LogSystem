using Microsoft.Extensions.Configuration;
namespace Log_system.Services.Repositories
{
    public class LogRepository:ILogRepository
    {
        private string connectionString;
        public LogRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("LogDB"); ;
        }

        public void CreateTable()
        {

        }
    }
}
