using Dapper;
using System.Data;
using Interfaces;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;

namespace DataAccessLayer
{
    public class RoleRepo : IRoleRepo
    {
        private readonly IDbConnection _connectAdmin;

        public RoleRepo(IDbChoixConnRepo connection)
        {
            _connectAdmin = connection.CreateConnection("ConnectDbAdmin");
        } 
        public async Task<List<T>> GetRoles<T>()
        {
            try
            {
                var roles = await _connectAdmin.QueryAsync<T>("[shAdmin].[SelectRoles]", commandType: CommandType.StoredProcedure);
                return roles.ToList();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }
       
    }
}
