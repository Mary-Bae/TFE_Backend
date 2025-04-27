using Models;

namespace Interfaces
{
    public interface IEmployeRepo
    {
        Task<T?> GetMailManagerByUser<T>(string auth0Id);
        Task<int> GetManagerId(string auth0Id);
        Task<T?> GetMailByDemande<T>(int demId);
        Task<List<T>> GetUsers<T>();
        Task<List<T>> GetManagers<T>();
        Task CreateUser(EmployeDTO employe);

    }
}
