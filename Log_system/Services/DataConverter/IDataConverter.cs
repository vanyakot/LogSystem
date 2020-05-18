using System.Threading.Tasks;

namespace Log_system.Services
{
    public interface IDataConverter
    {
        Task<T> WebResponseToObj<T>();
    }
}
