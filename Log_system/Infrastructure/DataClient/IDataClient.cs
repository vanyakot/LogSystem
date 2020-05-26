using System.Threading.Tasks;

namespace Log_system.Infrastucture
{
    public interface IDataClient
    {
        Task<T> GetResponse<T>();
    }
}
