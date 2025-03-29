using Interfaces;
using Dapper;
using System.Data;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;

namespace DataAccessLayer
{
    public class JoursFeriesRepo : IJoursFeriesRepo
    {
        private readonly IDbConnection _Connection;

        public JoursFeriesRepo(IDbConnection pConnection)
        {
            _Connection = pConnection;
        }
        public async Task<List<T>> GetJoursFeries<T>()
        {
            try
            {
                var lst = await _Connection.QueryAsync<T>("[shUser].[SelectJoursFeries]", commandType: CommandType.StoredProcedure);
            return lst.ToList();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }

    }
}
