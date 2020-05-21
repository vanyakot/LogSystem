using Log_system.Data.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Log_system.Services
{
    public class LogRepository : ILogRepository
    {
        private readonly string _connectionString;
        public LogRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LogDB");
        }

        public void AddError(LogData data, DateTime date)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                    INSERT INTO ErrorData
                        (HotelId,
                         Error,
                         Date)
                    VALUES 
                        (@hotelId, 
                         @error, 
                         @date)";

                    command.Parameters.Add("@hotelId", SqlDbType.Int).Value = data.HotelId;
                    command.Parameters.Add("@error", SqlDbType.NVarChar).Value = data.Error; ;
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = date;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddWarning(LogData data, DateTime date)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                    INSERT INTO WarningData
                        (HotelId,
                         Error,
                         Date)
                    VALUES 
                        (@hotelId, 
                         @warning, 
                         @date)";

                    command.Parameters.Add("@hotelId", SqlDbType.Int).Value = data.HotelId;
                    command.Parameters.Add("@warning", SqlDbType.NVarChar).Value = data.Warning; ;
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = date;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
