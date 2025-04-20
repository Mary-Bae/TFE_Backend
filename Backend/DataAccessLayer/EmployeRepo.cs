using Dapper;
using System.Data;
using Interfaces;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;

namespace DataAccessLayer
{
    public class EmployeRepo : IEmployeRepo
    {
        private readonly IDbConnection _connection;
        private readonly IDbConnection _connectManager;

        public EmployeRepo(IDbChoixConnRepo connection)
        {
            _connection = connection.CreateConnection("ConnectDb");
            _connectManager = connection.CreateConnection("ConnectDbManager");
        }
        public async Task<T?> GetMailManagerByUser<T>(string auth0Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Auth0Id", auth0Id);

               var eMail =  await _connection.QueryAsync<T>("[shUser].[SelectMailManager]", parameters, commandType: CommandType.StoredProcedure);
                return eMail.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }
        public async Task<int> GetManagerId(string auth0Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Auth0Id", auth0Id);
            var id = await _connection.QueryAsync<int>("[shUser].[SelectIdEmploye]", parameters, commandType: CommandType.StoredProcedure);
            return id.FirstOrDefault();
        }
        public async Task<T?> GetMailByDemande<T>(int demId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DemId", demId);

                var eMail = await _connectManager.QueryAsync<T>("[shManager].[SelectMailEmploye]", parameters, commandType: CommandType.StoredProcedure);
                return eMail.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }
    }
}
