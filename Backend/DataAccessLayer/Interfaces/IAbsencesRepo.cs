using Models;

namespace Interfaces
{
    public interface IAbsencesRepo
    {
        Task<List<T>> GetAbsences<T>();
        Task<List<T>> GetAbsencesByEmployeId<T>(int employeId);
        Task AddAbsence(TypeAbsenceDTO absence, int employeId);
        Task UpdAbsence(TypeAbsenceDTO absence, int employeId);
        Task DeleteAbsence(int pId);
        Task<T?> GetJoursCongesByContrat<T>(int employeId);
    }
}
