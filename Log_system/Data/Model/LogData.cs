using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log_system.Data.Model
{
    public class LogData
    {
        public int HotelId { get; set; }
        public string Error { get; set; }
        public string Warning { get; set; }
    }
}
