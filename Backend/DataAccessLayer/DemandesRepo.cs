using Interfaces;
using Dapper;
using System.Data;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;

namespace DataAccessLayer
{
    public class DemandesRepo : IDemandesRepo
    {
        private readonly IDbConnection _Connection;

        public DemandesRepo(IDbConnection pConnection)
        {
            _Connection = pConnection;
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
        public async Task AddDemandeAbs(AddAndUpdDemandeDTO demande, string auth0Id, decimal duree)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@auth0Id", auth0Id);
                parameters.Add("@DEM_DteDebut", demande.DEM_DteDebut);
                parameters.Add("@DEM_DteFin", demande.DEM_DteFin);
                parameters.Add("@DEM_Comm", demande.DEM_Comm);
                parameters.Add("@DEM_TYPE_id", demande.DEM_TYPE_id);
                parameters.Add("@DEM_Justificatif", demande.DEM_Justificatif);
                parameters.Add("@DEM_DureeHeures", duree);

                await _Connection.ExecuteAsync("[shUser].[AddDemande]", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }
        public async Task<T?> GetDemandeById<T>(int demandeId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DEM_id", demandeId);

                var demande = await _Connection.QuerySingleOrDefaultAsync<T>("[shUser].[SelectDemandeById]", parameters, commandType: CommandType.StoredProcedure);
                return demande;
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }
        public async Task UpdateDemande(int pId, AddAndUpdDemandeDTO demande, decimal duree)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DEM_id", pId);
                parameters.Add("@DEM_DteDebut", demande.DEM_DteDebut);
                parameters.Add("@DEM_DteFin", demande.DEM_DteFin);
                parameters.Add("@DEM_Comm", demande.DEM_Comm);
                parameters.Add("@DEM_TYPE_id", demande.DEM_TYPE_id);
                parameters.Add("@DEM_Justificatif", demande.DEM_Justificatif);
                parameters.Add("@DEM_DureeHeures", duree);

                await _Connection.ExecuteAsync("[shUser].[UpdateDemande]", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : ", ex);
            }
        }
        public async Task DeleteDemande(int pId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DEM_id", pId);

                await _Connection.ExecuteAsync("[shUser].[DeleteDemande]", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : ", ex);

            }
        }

    }
}
