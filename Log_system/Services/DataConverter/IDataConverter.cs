using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log_system.Services.DataConverter
{
    interface IDataConverter
    {
        public T WebResponseToObj<T>();
    }
}
