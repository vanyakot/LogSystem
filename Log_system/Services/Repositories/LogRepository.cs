using Log_system.Data.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        public void AddError(LogData data)
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
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = data.TimeRecieve;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddWarning(LogData data)
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
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = data.TimeRecieve;

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<LogData> FindErrorsById(int HotelId)
        {
            List<LogData> Errors = new List<LogData>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"SELECT
                            HotelId,
                            Error,
                            Date
                        FROM ErrorData
                        WHERE HotelId = @hotelId";

                    command.Parameters.Add("@hotelId", SqlDbType.Int).Value = HotelId;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Error = new LogData
                            {
                                HotelId = Convert.ToInt32(reader["HotelId"]),
                                Error = Convert.ToString(reader["Error"]),
                                TimeRecieve = Convert.ToDateTime(reader["Date"]),
                            };
                            Errors.Add(Error);
                        }
                    }
                }
            }

            return Errors;
        }

        public List<LogData> FindErrorsByDate(DateTime StartDate, DateTime EndDate)
        {
            List<LogData> Errors = new List<LogData>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"SELECT
                            HotelId,
                            Error,
                            Date
                        FROM ErrorData
                        WHERE Date >= @startDate AND Date <= @endDate";

                    command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = StartDate;
                    command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = EndDate;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Error = new LogData
                            {
                                HotelId = Convert.ToInt32(reader["HotelId"]),
                                Error = Convert.ToString(reader["Error"]),
                                TimeRecieve = Convert.ToDateTime(reader["Date"]),
                            };
                            Errors.Add(Error);
                        }
                    }
                }
            }

            return Errors;
        }

        public List<LogData> FindErrorsByIdAndDate(int HotelId, DateTime StartDate, DateTime EndDate)
        {
            List<LogData> Errors = new List<LogData>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"SELECT
                            HotelId,
                            Error,
                            Date
                        FROM ErrorData
                        WHERE HotelId = @hotelId AND Date >= @startDate AND Date <= @endDate";

                    command.Parameters.Add("@hotelId", SqlDbType.Int).Value = HotelId;
                    command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = StartDate;
                    command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = EndDate;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Error = new LogData
                            {
                                HotelId = Convert.ToInt32(reader["HotelId"]),
                                Error = Convert.ToString(reader["Error"]),
                                TimeRecieve = Convert.ToDateTime(reader["Date"]),
                            };
                            Errors.Add(Error);
                        }
                    }
                }
            }

            return Errors;
        }
    }
}
