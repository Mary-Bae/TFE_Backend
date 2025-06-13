using CustomErrors;
using Microsoft.Data.SqlClient;
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
        public async Task AddAbsence(TypeAbsenceDTO absence, int employeId, decimal jours)
        {
                if (absence.TAEM_TYPE_id == 2) // Télétravail
                {
                    if (jours > 5)
                        throw new CustomError(ErreurCodeEnum.JoursEnTrop);
                    absence.TAEM_NbrJoursSemaine = jours;
                    absence.TAEM_NbrJoursAn = null;
            }
                else
                {
                    absence.TAEM_NbrJoursAn = jours;
                    absence.TAEM_NbrJoursSemaine = null;
            }

                await _absencesRepo.AddAbsence(absence, employeId);
        }
        public async Task UpdAbsence(TypeAbsenceDTO absence, int employeId, decimal jours)
        {

            if (absence.TAEM_TYPE_id == 2)
            {
                if (jours > 5)
                    throw new CustomError(ErreurCodeEnum.JoursEnTrop);
                absence.TAEM_NbrJoursSemaine = jours;
            }
            else
            {
                absence.TAEM_NbrJoursAn = jours;
            }

            await _absencesRepo.UpdAbsence(absence, employeId);
        }
        public async Task DeleteAbsence(int pId)
        {
            try
            {
                await _absencesRepo.DeleteAbsence(pId);
            }
            catch (SqlException ex)
            {
                if (ex.Message.StartsWith("[ABS01]"))
                    throw new CustomError(ErreurCodeEnum.DemandesExistantes, ex);
                throw new CustomError(ErreurCodeEnum.ErreurSQL, ex);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }
        public async Task<JoursParContratDTO?> GetJoursCongesSuggérés(int employeId, int typeAbsenceId)
        {
            // Télétravail
            if (typeAbsenceId == 2)
                return new JoursParContratDTO { JoursSuggérés = 3 };
            // Congé de maternité
            if (typeAbsenceId == 4)
                return new JoursParContratDTO { JoursSuggérés = 75 };

            // Congé de paternité
            if (typeAbsenceId == 5)
                return new JoursParContratDTO { JoursSuggérés = 20 };

            // Congé légal => appeler la procédure stockée
            if (typeAbsenceId == 1)
            {
                var result = await _absencesRepo.GetJoursCongesByContrat<JoursParContratDTO>(employeId);

                if (result == null)
                    throw new CustomError(ErreurCodeEnum.AucuneSuggestion);
                return result;
            }
            // Pour les autres types d’absences : pas de suggestion
            return null;
        }
    }
}
