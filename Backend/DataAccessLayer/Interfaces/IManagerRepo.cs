using Models;

namespace Interfaces
{
    public interface IManagerRepo
    {
        Task<T?> GetMailManagerByUser<T>(string auth0Id);
        Task<int> GetManagerId(string auth0Id);

    }
}
