using Log_system.Data.Model;
using Log_system.Infrastucture;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Log_system.Services.Repositories
{
    public class ErrorRepository : IErrorRepository
    {
        private readonly string _connectionString;
        public ErrorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LogDB");
        }

        public void AddError(LogError data)
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
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = data.CreationDateTime;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddErrorWithInfo(LogError data)
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
                         Date,
                         AdditionalInfo)
                    VALUES 
                        (@hotelId, 
                         @error, 
                         @date,
                         @additionalInfo)";

                    command.Parameters.Add("@hotelId", SqlDbType.Int).Value = data.HotelId;
                    command.Parameters.Add("@error", SqlDbType.NVarChar).Value = data.Error; ;
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = data.CreationDateTime;
                    command.Parameters.Add("@additionalInfo", SqlDbType.NVarChar).Value = data.AdditionalInfo;

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<LogError> FindErrorsByDate(DateTime StartDate, DateTime EndDate)
        {
            List<LogError> Errors = new List<LogError>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"SELECT
                            HotelId,
                            Error,
                            Date,
                            AdditionalInfo
                        FROM ErrorData
                        WHERE Date >= @startDate AND Date <= @endDate";

                    command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = StartDate;
                    command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = EndDate;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Error = new LogError
                            {
                                HotelId = Convert.ToInt32(reader["HotelId"]),
                                Error = Convert.ToString(reader["Error"]),
                                CreationDateTime = Convert.ToDateTime(reader["Date"]),
                                AdditionalInfo = Convert.ToString(reader["AdditionalInfo"]),
                            };
                            Errors.Add(Error);
                        }
                    }
                }
            }

            return Errors;
        }

        public List<LogError> FindErrorsByIdAndDate(int HotelId, DateTime StartDate, DateTime EndDate)
        {
            List<LogError> Errors = new List<LogError>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"SELECT
                            HotelId,
                            Error,
                            Date,
                            AdditionalInfo
                        FROM ErrorData
                        WHERE HotelId = @hotelId AND Date >= @startDate AND Date <= @endDate";

                    command.Parameters.Add("@hotelId", SqlDbType.Int).Value = HotelId;
                    command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = StartDate;
                    command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = EndDate;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Error = new LogError
                            {
                                HotelId = Convert.ToInt32(reader["HotelId"]),
                                Error = Convert.ToString(reader["Error"]),
                                CreationDateTime = Convert.ToDateTime(reader["Date"]),
                                AdditionalInfo = Convert.ToString(reader["AdditionalInfo"]),
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
