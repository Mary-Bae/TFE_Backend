using Dapper;
using System.Data;
using Interfaces;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using CustomErrors;

namespace DataAccessLayer
{
    public class AbsencesRepo : IAbsencesRepo
    {
        private readonly IDbConnection _connectAdmin;

        public AbsencesRepo(IDbChoixConnRepo connection)
        {
            _connectAdmin = connection.CreateConnection("ConnectDbAdmin");
        }
        public async Task<List<T>> GetAbsences<T>()
        {
                var absences = await _connectAdmin.QueryAsync<T>("[shAdmin].[SelectTypeAbsence]", commandType: CommandType.StoredProcedure);
                return absences.ToList();
        }
        public async Task<List<T>> GetAbsencesByEmployeId<T>(int employeId)
        {
                var parameters = new DynamicParameters();
                parameters.Add("@EMP_id", employeId);

                var employe = await _connectAdmin.QueryAsync<T>("[shAdmin].[AbsencesByEmployeId]", parameters, commandType: CommandType.StoredProcedure);
                return employe.ToList();
        }
        public async Task AddAbsence(TypeAbsenceDTO absence, int employeId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@EMP_id", employeId);
                parameters.Add("@TYPE_id", absence.TAEM_TYPE_id);
                parameters.Add("@TAEM_NbrJoursAn", absence.TAEM_NbrJoursAn);
                parameters.Add("@TAEM_NbrJoursSemaine", absence.TAEM_NbrJoursSemaine);

                await _connectAdmin.ExecuteAsync("[shAdmin].[AddAbsence]", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }
        public async Task UpdAbsence(TypeAbsenceDTO absence, int employeId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@EMP_id", employeId);
                parameters.Add("@TYPE_id", absence.TAEM_TYPE_id);
                parameters.Add("@TAEM_NbrJoursAn", absence.TAEM_NbrJoursAn);
                parameters.Add("@TAEM_NbrJoursSemaine", absence.TAEM_NbrJoursSemaine);

                await _connectAdmin.ExecuteAsync("[shAdmin].[UpdateAbsence]", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }
        public async Task DeleteAbsence(int pId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", pId);

                await _connectAdmin.ExecuteAsync("[shAdmin].[DeleteAbsence]", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                if (ex.Message == "DemandesExists")
                    throw new CustomError(ErreurCodeEnum.DemandesExistantes, ex);
                throw new CustomError(ErreurCodeEnum.ErreurSQL, ex);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }

    }
}
