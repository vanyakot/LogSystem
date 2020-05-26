using System;

namespace Log_system.Infrastucture
{
    public class LogData
    {
        public int HotelId { get; set; }
        public string Error { get; set; }
        public string AdditionalInfo { get; set; }
        public string Warning { get; set; }
        public DateTime TimestampUtc { get; set; }

    }
}