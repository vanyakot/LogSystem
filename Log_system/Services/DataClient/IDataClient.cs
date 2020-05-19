using System.Threading.Tasks;

namespace Log_system.Services
{
    public interface IDataClient
    {
        Task<T> GetResponse<T>();
    }
}
