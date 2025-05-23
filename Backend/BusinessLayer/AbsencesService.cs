using DataAccessLayer;
using Interfaces;
using Models;

namespace BusinessLayer
{
    public class AbsencesService : IAbsencesService
    {
        private readonly IAbsencesRepo _absencesRepo;

        public AbsencesService(IAbsencesRepo absencesRepo)
        {
            _absencesRepo = absencesRepo;
        }
        public async Task<List<T>> GetAbsences<T>()
        {
            return await _absencesRepo.GetAbsences<T>();
        }
        public async Task<List<T>> GetAbsencesByEmployeId<T>(int employeId)
        {
            return await _absencesRepo.GetAbsencesByEmployeId<T>(employeId);
        }
    }
}
