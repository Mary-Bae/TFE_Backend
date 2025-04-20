using Models;

namespace Interfaces
{
    public interface IDemandesRepo
    {
        Task<List<T>> GetDemandesByUser<T>(string auth0Id);
        Task<List<T>> GetTypeAbsByUser<T>(string auth0Id);
        Task<List<T>> GetDemandesEquipe<T>(int managerId);
        Task<T?> GetDemandeById<T>(int demandeId);
        Task AddDemandeAbs(AddAndUpdDemandeDTO demande, string auth0Id, decimal duree);
        Task UpdateDemande(int pId, AddAndUpdDemandeDTO demande, decimal duree);
        Task DeleteDemande(int pId);
        Task UpdStatusDemande(int pId, int pStatut);

    }
}
