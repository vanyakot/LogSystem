using System;

namespace Log_system.Data.Model
{
    public class LogData
    {
        public int HotelId { get; set; }
        public string Error { get; set; }
        public string Warning { get; set; }
        public DateTime TimeRecieve { get; set; }
    }
}