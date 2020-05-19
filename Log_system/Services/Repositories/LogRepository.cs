using Microsoft.Extensions.Configuration;
namespace Log_system.Services.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly string _connectionString;
        public LogRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LogDB"); ;
        }

        public void CreateTable()
        {

        }
    }
}
