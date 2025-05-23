using Models;

namespace Interfaces
{
    public interface IAbsencesRepo
    {
        Task<List<T>> GetAbsences<T>();
        Task<List<T>> GetAbsencesByEmployeId<T>(int employeId);
    }
}
