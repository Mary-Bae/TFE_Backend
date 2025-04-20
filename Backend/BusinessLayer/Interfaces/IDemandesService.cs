using Models;

namespace Interfaces
{
    public interface IDemandesService
    {
        Task<List<T>> GetDemandesByUser<T>(string auth0Id);
        Task<List<T>> GetDemandesEquipe<T>(string auth0Id);
        Task<T?> GetDemandeById<T>(int demandeId);
        Task<List<T>> GetTypeAbsByUser<T>(string auth0Id);
        Task AddDemandeAbs(AddAndUpdDemandeDTO demande, string auth0Id);
        Task UpdateDemande(int pId, AddAndUpdDemandeDTO demande);
        Task DeleteDemande(int pId);
        Task UpdStatusDemande(int pId, int pStatut);
    }
}
