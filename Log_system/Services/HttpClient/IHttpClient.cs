using System.Net;
using System.Threading.Tasks;

namespace Log_system.Services
{
    public interface IHttpClient
    {
        Task<WebResponse> GetHttpResponseMessage();
    }
}
