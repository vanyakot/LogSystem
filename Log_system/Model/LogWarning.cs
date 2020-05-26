using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log_system.Model
{
    public class LogWarning
    {
        public int HotelId { get; set; }
        public string Warning { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
