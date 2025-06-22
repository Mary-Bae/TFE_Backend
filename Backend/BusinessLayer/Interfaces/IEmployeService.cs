using Models;

namespace Interfaces
{
    public interface IEmployeService
    {
        Task<T?> GetMailManagerByUser<T>(string auth0Id);
        Task<T?> GetMailByDemande<T>(int demId);
        Task<List<T>> GetUsers<T>();
        Task<List<T>> GetDeletedUsers<T>();
        Task<List<T>> GetManagers<T>();
        Task<EmployeDTO> CreateUser(EmployeDTO employe);
        Task<EmployeDTO> UpdateEmploye(int pId, EmployeDTO employe);
        Task<T?> GetEmployeById<T>(int employeId);
        Task DeleteEmploye(int pId);
        Task RestoreEmploye(int pId);
    }
}