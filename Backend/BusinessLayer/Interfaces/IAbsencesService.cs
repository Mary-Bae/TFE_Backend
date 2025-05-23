using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IAbsencesService
    {
        Task<List<T>> GetAbsences<T>();
        Task<List<T>> GetAbsencesByEmployeId<T>(int employeId);
        Task AddAbsence(TypeAbsenceDTO absence, int employeId, decimal jours);
        Task UpdAbsence(TypeAbsenceDTO absence, int employeId, decimal jours);
        Task DeleteAbsence(int pId);
    }
}
