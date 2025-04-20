using Interfaces;
using Dapper;
using System.Data;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;
using Microsoft.AspNetCore.Connections;

namespace DataAccessLayer
{
    public class CompteurRepo : ICompteurRepo
    {
        private readonly IDbConnection _connection;

        public CompteurRepo(IDbChoixConnRepo connection)
        {
            _connection = connection.CreateConnection("ConnectDb");
        }
        public async Task<List<T>> GetCompteurByUser<T>(string auth0Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Auth0Id", auth0Id);

                var lst = await _connection.QueryAsync<T>("[shUser].[SelectCompteur]", parameters, commandType: CommandType.StoredProcedure);
                return lst.ToList();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }
    }
}
