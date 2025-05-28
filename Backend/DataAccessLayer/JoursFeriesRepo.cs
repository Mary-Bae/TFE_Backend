using Interfaces;
using Dapper;
using System.Data;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;
using System.Data.Common;

namespace DataAccessLayer
{
    public class JoursFeriesRepo : IJoursFeriesRepo
    {
        private readonly IDbConnection _connection;

        public JoursFeriesRepo(IDbChoixConnRepo connection)
        {
            _connection = connection.CreateConnection("ConnectDb");
        }
        public async Task<List<T>> GetJoursFeries<T>()
        {
            var lst = await _connection.QueryAsync<T>("[shUser].[SelectJoursFeries]", commandType: CommandType.StoredProcedure);
            return lst.ToList();
        }

    }
}
