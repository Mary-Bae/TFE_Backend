using Interfaces;
using Models;

namespace BusinessLayer
{
    public class DemandesService : IDemandesService
    {
        private readonly IDemandesRepo _demandeRepo;
        private readonly IManagerRepo _managerRepo;

        public DemandesService(IDemandesRepo demandesRepo, IManagerRepo managerRepo)
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
            decimal duree = 0;

            switch (demande.TypeJournee)
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
                    throw new ArgumentException("Type de journée non valide.");
            }

            await _demandeRepo.AddDemandeAbs(demande, auth0Id, duree);
        }
        public async Task UpdateDemande(int pId, AddAndUpdDemandeDTO demande)
        {
            decimal duree = 0;

            switch (demande.TypeJournee)
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
                    throw new ArgumentException("Type de journée non valide.");
            }
            await _demandeRepo.UpdateDemande(pId, demande, duree);
        }
        public async Task DeleteDemande(int pId)
        {
            await _demandeRepo.DeleteDemande(pId);
        }
        public async Task UpdStatusDemande(int pId, int pStatut)
        {
            await _demandeRepo.UpdStatusDemande(pId, pStatut);
        }
    }
}
