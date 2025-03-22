using Models;

namespace Interfaces
{
    public interface IDemandesService
    {
        //void Add(Demandes dto);
        Task<List<T>> GetDemandes<T>();
        Task<List<T>> GetDemandesByUser<T>(string auth0Id);
        Task<T?> GetDemandeById<T>(int demandeId);
        Task<List<T>> GetTypeAbsByUser<T>(string auth0Id);
        Task AddDemandeAbs(AddDemandeDTO demande, string auth0Id);
    }
}
