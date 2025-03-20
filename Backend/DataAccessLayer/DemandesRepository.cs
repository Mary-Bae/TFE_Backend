using Interfaces;
using Domain;
using Dapper;
using System.Data;
using System.Security.Cryptography;
using System.Reflection.Metadata;

namespace DataAccessLayer
{
    public class DemandesRepository : IDemandesRepository
    {
        IDbConnection _Connection;

        public DemandesRepository(IDbConnection pConnection)
        {
            _Connection = pConnection;
        }
        public async Task<List<T>> GetDemandes<T>()
        {
            var lst = await _Connection.QueryAsync<T>("[shUser].[SelectDemande]");
            return lst.ToList();
        }
        public async Task<List<T>> GetDemandesByUser<T>(string auth0Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Auth0Id", auth0Id);

                var lst = await _Connection.QueryAsync<T>("[shUser].[SelectDemandeByUser]", parameters, commandType: CommandType.StoredProcedure);
            return lst.ToList();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }
        public async Task<List<T>> GetTypeAbsByUser<T>(string auth0Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Auth0Id", auth0Id);

                var lst = await _Connection.QueryAsync<T>("[shUser].[TypeAbsenceByUser]", parameters, commandType: CommandType.StoredProcedure);
                return lst.ToList();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }

        //public Demandes GetDemandeById(int id)
        //{
        //    return _dbProvisoire.FirstOrDefault(d => d.Id == id);
        //}
        //public void Add(Demandes dto)
        //{
        //    var demande = new Demandes(dto.Type)
        //    {
        //        Id = _dbProvisoire.Count + 1,
        //        Type = dto.Type,
        //        DateBegin = dto.DateBegin,
        //        DateEnd = dto.DateEnd,
        //        Comment = dto.Comment
        //    };
        //    _dbProvisoire.Add(demande);
        //}

    }
}
