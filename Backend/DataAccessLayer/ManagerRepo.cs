using Dapper;
using System.Data;
using Interfaces;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;

namespace DataAccessLayer
{
    public class ManagerRepo : IManagerRepo
    {
        private readonly IDbConnection _connection;

        public ManagerRepo(IDbChoixConnRepo connection)
        {
            _connection = connection.CreateConnection("ConnectDb");
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
    }
}
