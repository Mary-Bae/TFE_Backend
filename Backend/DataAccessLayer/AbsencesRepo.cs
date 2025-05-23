using Dapper;
using System.Data;
using Interfaces;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;

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
            try
            {
                var absences = await _connectAdmin.QueryAsync<T>("[shAdmin].[SelectTypeAbsence]", commandType: CommandType.StoredProcedure);
                return absences.ToList();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }
        public async Task<List<T>> GetAbsencesByEmployeId<T>(int employeId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@EMP_id", employeId);

                var employe = await _connectAdmin.QueryAsync<T>("[shAdmin].[AbsencesByEmployeId]", parameters, commandType: CommandType.StoredProcedure);
                return employe.ToList();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }

    }
}
