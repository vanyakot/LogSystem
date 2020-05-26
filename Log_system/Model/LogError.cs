using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log_system.Data.Model
{
    public class LogError
    {
        public int HotelId { get; set; }
        public string Error { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
