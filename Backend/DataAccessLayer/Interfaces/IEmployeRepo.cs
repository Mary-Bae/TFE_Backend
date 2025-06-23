using Models;

namespace Interfaces
{
    public interface IEmployeRepo
    {
        Task<T?> GetMailManagerByUser<T>(string auth0Id);
        Task<int> GetManagerId(string auth0Id);
        Task<T?> GetMailByDemande<T>(int demId);
        Task<List<T>> GetUsers<T>();
        Task<List<T>> GetDeletedUsers<T>();
        Task<List<T>> GetManagers<T>();
        Task CreateUser(EmployeDTO employe);
        Task UpdateEmploye(int pId, EmployeDTO employe);
        Task<T?> GetEmployeById<T>(int employeId);
        Task DeleteEmploye(int pId, int? modifiedBy);
        Task RestoreEmploye(int pId, int? modifiedBy);
        Task<T?> GetUserByAuth<T>(string auth0Id);

    }
}
