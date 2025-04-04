﻿using Dapper;
using System.Data;
using Interfaces;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;

namespace DataAccessLayer
{
    public class ManagerRepo : IManagerRepo
    {
        private readonly IDbConnection _Connection;

        public ManagerRepo(IDbConnection pConnection)
        {
            _Connection = pConnection;
        }
        public async Task<T?> GetMailManagerByUser<T>(string auth0Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Auth0Id", auth0Id);

               var eMail =  await _Connection.QueryAsync<T>("[shUser].[SelectMailManager]", parameters, commandType: CommandType.StoredProcedure);
                return eMail.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }
    }
}
