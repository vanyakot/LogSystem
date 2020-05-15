using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log_system.Services
{
    public interface IHttpClient
    {
        string _response { get; set; }

        Task SendGetRequest();
    }
}
