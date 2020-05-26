using System.Net;
using System.Threading.Tasks;

namespace Log_system.Infrastucture
{
    public interface IHttpClient
    {
        Task<WebResponse> GetHttpResponseMessage();
    }
}
