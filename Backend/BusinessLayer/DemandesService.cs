using CustomErrors;
using Interfaces;
using Models;
using Microsoft.Data.SqlClient;

namespace BusinessLayer
{
    public class DemandesService : IDemandesService
    {
        private readonly IDemandesRepo _demandeRepo;
        private readonly IEmployeRepo _managerRepo;

        public DemandesService(IDemandesRepo demandesRepo, IEmployeRepo managerRepo)
        {
            _demandeRepo = demandesRepo;
            _managerRepo = managerRepo;
        }
        public async Task<List<T>> GetDemandesByUser<T>(string auth0Id)
        {
                return await _demandeRepo.GetDemandesByUser<T>(auth0Id);
        }
        public async Task<List<T>> GetDemandesEquipe<T>(string auth0Id)
        {
            var managerId = await _managerRepo.GetManagerId(auth0Id);
            return await _demandeRepo.GetDemandesEquipe<T>(managerId);
        }
        public async Task<T?> GetDemandeById<T>(int demandeId)
        {
            return await _demandeRepo.GetDemandeById<T>(demandeId);
        }
        public async Task<List<T>> GetTypeAbsByUser<T>(string auth0Id)
        {
            return await _demandeRepo.GetTypeAbsByUser<T>(auth0Id);
        }
        public async Task AddDemandeAbs(AddAndUpdDemandeDTO demande, string auth0Id)
        {
            try 
            {
                decimal duree = 0;

                switch (demande.DEM_TypeJournee)
                {
                    case "Journee":
                        duree = 8;
                        break;
                    case "Matin":
                        duree = 4;
                        break;
                    case "Apres-midi":
                        duree = 4;
                        break;
                    default:
                        throw new CustomError(ErreurCodeEnum.TypeJournee);
                }

                await _demandeRepo.AddDemandeAbs(demande, auth0Id, duree);
            }
            catch (SqlException ex)
            {
                if (ex.Message.StartsWith("[DEM01]"))
                    throw new CustomError(ErreurCodeEnum.DemandesPassé, ex);
                if (ex.Message.StartsWith("[DEM02]"))
                    throw new CustomError(ErreurCodeEnum.DatesSimilaires, ex);
                if (ex.Message.StartsWith("[DEM03]"))
                    throw new CustomError(ErreurCodeEnum.SoldeInexistant, ex);
                if (ex.Message.StartsWith("[DEM04]"))
                    throw new CustomError(ErreurCodeEnum.HeuresRestant, ex);
                if (ex.Message.StartsWith("[DEM06]"))
                    throw new CustomError(ErreurCodeEnum.HeuresHebdo, ex);
                throw new CustomError(ErreurCodeEnum.ErreurSQL, ex);

            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }
        public async Task UpdateDemande(int pId, AddAndUpdDemandeDTO demande)
        {
            try 
            {
                decimal duree = 0;

                switch (demande.DEM_TypeJournee)
                {
                    case "Journee":
                        duree = 8;
                        break;
                    case "Matin":
                        duree = 4;
                        break;
                    case "Apres-midi":
                        duree = 4;
                        break;
                    default:
                        throw new CustomError(ErreurCodeEnum.TypeJournee);
                }
                await _demandeRepo.UpdateDemande(pId, demande, duree);

            }
            catch (SqlException ex)
            {
                if(ex.Message.StartsWith("[DEM05]"))
                    throw new CustomError(ErreurCodeEnum.ModifierDemEnAttente, ex);
                if (ex.Message.StartsWith("[DEM01]"))
                    throw new CustomError(ErreurCodeEnum.DemandesPassé, ex);
                if (ex.Message.StartsWith("[DEM02]"))
                    throw new CustomError(ErreurCodeEnum.DatesSimilaires, ex);
                if (ex.Message.StartsWith("[DEM03]"))
                    throw new CustomError(ErreurCodeEnum.SoldeInexistant, ex);
                if (ex.Message.StartsWith("[DEM04]"))
                    throw new CustomError(ErreurCodeEnum.HeuresRestant, ex);
                if (ex.Message.StartsWith("[DEM06]"))
                    throw new CustomError(ErreurCodeEnum.HeuresHebdo, ex);
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
                await _demandeRepo.DeleteDemande(pId);
            }
            catch (SqlException ex)
            {
                if (ex.Message.StartsWith("[DEM05]"))
                    throw new CustomError(ErreurCodeEnum.ModifierDemEnAttente, ex);
                throw new CustomError(ErreurCodeEnum.ErreurSQL, ex);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }
        public async Task UpdStatusDemande(int pId, int pStatut)
        {
            await _demandeRepo.UpdStatusDemande(pId, pStatut);
        }
    }
}
