using Log_system.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Log_system.Services.Repositories
{
    public class WarningRepository : IWarningRepository
    {
        private readonly string _connectionString;
        public WarningRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LogDB");
        }

        public void AddWarning(LogWarning data)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                    INSERT INTO WarningData
                        (HotelId,
                         Warning,
                         Date)
                    VALUES 
                        (@hotelId, 
                         @warning, 
                         @date)";

                    command.Parameters.Add("@hotelId", SqlDbType.Int).Value = data.HotelId;
                    command.Parameters.Add("@warning", SqlDbType.NVarChar).Value = data.Warning; ;
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = data.CreationDateTime;

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<LogWarning> FindWarningsByDate(DateTime StartDate, DateTime EndDate)
        {
            List<LogWarning> Warnings = new List<LogWarning>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"SELECT
                            HotelId,
                            Warning,
                            Date
                        FROM WarningData
                        WHERE Date >= @startDate AND Date <= @endDate";

                    command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = StartDate;
                    command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = EndDate;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Warning = new LogWarning
                            {
                                HotelId = Convert.ToInt32(reader["HotelId"]),
                                Warning = Convert.ToString(reader["Warning"]),
                                CreationDateTime = Convert.ToDateTime(reader["Date"]),
                            };
                            Warnings.Add(Warning);
                        }
                    }
                }
            }

            return Warnings;
        }

        public List<LogWarning> FindWarningsByIdAndDate(int HotelId, DateTime StartDate, DateTime EndDate)
        {
            List<LogWarning> Warnings = new List<LogWarning>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"SELECT
                            HotelId,
                            Warning,
                            Date
                        FROM WarningData
                        WHERE HotelId = @hotelId AND Date >= @startDate AND Date <= @endDate";

                    command.Parameters.Add("@hotelId", SqlDbType.Int).Value = HotelId;
                    command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = StartDate;
                    command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = EndDate;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Warning = new LogWarning
                            {
                                HotelId = Convert.ToInt32(reader["HotelId"]),
                                Warning = Convert.ToString(reader["Warning"]),
                                CreationDateTime = Convert.ToDateTime(reader["Date"]),
                            };
                            Warnings.Add(Warning);
                        }
                    }
                }
            }

            return Warnings;
        }
    }
}


