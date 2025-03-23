using Interfaces;
using Models;

namespace BusinessLayer
{
    public class DemandesService : IDemandesService
    {
        private readonly IDemandesRepository _demandeRepo;

        public DemandesService(IDemandesRepository demandesRepo)
        {
            _demandeRepo = demandesRepo;
        }
        public async Task<List<T>> GetDemandesByUser<T>(string auth0Id)
        {
            return await _demandeRepo.GetDemandesByUser<T>(auth0Id);
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
            await _demandeRepo.AddDemandeAbs(demande, auth0Id);
        }
        public async Task UpdateDemande(int pId, AddAndUpdDemandeDTO demande)
        {
            await _demandeRepo.UpdateDemande(pId, demande);
        }
        public async Task DeleteDemande(int pId)
        {
            await _demandeRepo.DeleteDemande(pId);
        }
    }
}
