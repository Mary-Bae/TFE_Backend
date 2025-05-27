using Interfaces;
using Dapper;
using System.Data;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using CustomErrors;

namespace DataAccessLayer
{
    public class DemandesRepo : IDemandesRepo
    {
        private readonly IDbConnection _connection;
        private readonly IDbConnection _connectManager;

        public DemandesRepo(IDbChoixConnRepo connection)
        {
            _connection = connection.CreateConnection("ConnectDb");
            _connectManager = connection.CreateConnection("ConnectDbManager");
        }
        public async Task<List<T>> GetDemandesByUser<T>(string auth0Id)
        {
            try
            {
                 var parameters = new DynamicParameters();
                parameters.Add("@Auth0Id", auth0Id);

                var lst = await _connection.QueryAsync<T>("[shUser].[SelectDemandeByUser]", parameters, commandType: CommandType.StoredProcedure);
                return lst.ToList();
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }
        public async Task<List<T>> GetTypeAbsByUser<T>(string auth0Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Auth0Id", auth0Id);

                var lst = await _connection.QueryAsync<T>("[shUser].[TypeAbsenceByUser]", parameters, commandType: CommandType.StoredProcedure);
                return lst.ToList();
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
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
                parameters.Add("@DEM_TypeJournee", demande.DEM_TypeJournee);

                await _connection.ExecuteAsync("[shUser].[AddDemande]", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                if (ex.Message == "JourPassé")
                    throw new CustomError(ErreurCodeEnum.DemandesPassé, ex);
                if (ex.Message == "DatesSimilaires")
                    throw new CustomError(ErreurCodeEnum.DatesSimilaires, ex);
                if (ex.Message == "SoldeNotExists")
                    throw new CustomError(ErreurCodeEnum.SoldeInexistant, ex);
                if (ex.Message == "HeuresRestant")
                    throw new CustomError(ErreurCodeEnum.HeuresRestant, ex);
                throw new CustomError(ErreurCodeEnum.ErreurSQL, ex);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }
        public async Task<T?> GetDemandeById<T>(int demandeId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DEM_id", demandeId);

                var demande = await _connection.QuerySingleOrDefaultAsync<T>("[shUser].[SelectDemandeById]", parameters, commandType: CommandType.StoredProcedure);
                return demande;
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
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
                parameters.Add("@DEM_TypeJournee", demande.DEM_TypeJournee);

                await _connection.ExecuteAsync("[shUser].[UpdateDemande]", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                if(ex.Message == "PasEnAttente")
                    throw new CustomError(ErreurCodeEnum.ModifierDemEnAttente, ex);
                if (ex.Message == "JourPassé")
                    throw new CustomError(ErreurCodeEnum.DemandesPassé, ex);
                if (ex.Message == "DatesSimilaires")
                    throw new CustomError(ErreurCodeEnum.DatesSimilaires, ex);
                if (ex.Message == "SoldeNotExists")
                    throw new CustomError(ErreurCodeEnum.SoldeInexistant, ex);
                if (ex.Message == "HeuresRestant")
                    throw new CustomError(ErreurCodeEnum.HeuresRestant, ex);
                throw new CustomError(ErreurCodeEnum.ErreurSQL, ex);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }
        public async Task DeleteDemande(int pId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DEM_id", pId);

                await _connection.ExecuteAsync("[shUser].[DeleteDemande]", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                if (ex.Message == "PasEnAttente")
                    throw new CustomError(ErreurCodeEnum.ModifierDemEnAttente, ex);
                throw new CustomError(ErreurCodeEnum.ErreurSQL, ex);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }
        public async Task<List<T>> GetDemandesEquipe<T>(int managerId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ManagerId", managerId);

                var lst = await _connectManager.QueryAsync<T>("[shManager].[SelectDemandesEquipe]", parameters, commandType: CommandType.StoredProcedure);
                return lst.ToList();
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }
        public async Task UpdStatusDemande(int pId, int pStatut)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DemandeId", pId);
                parameters.Add("@Statut", pStatut);
                await _connectManager.ExecuteAsync("[shManager].[AcceptRefusDemandes]", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurSQL, ex);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }

    }
}
