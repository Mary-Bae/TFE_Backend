using Interfaces;
using Dapper;
using System.Data;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class DbChoixConnRepo : IDbChoixConnRepo
    {
        private readonly IConfiguration _configuration;

        public DbChoixConnRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection(string name)
        {
            var connectionString = _configuration.GetConnectionString(name);
            return new SqlConnection(connectionString);
        }
    }
}
