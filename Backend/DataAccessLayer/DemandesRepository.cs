using Interfaces;
using Domain;
using Dapper;
using System.Data;

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
