using Domain;
using Models;

namespace Interfaces
{
    public interface IDemandesRepository
    {
        //void Add(Demandes demande);
        Task<List<T>> GetDemandes<T>();
        Task<List<T>> GetDemandesByUser<T>(string auth0Id);
        Task<List<T>> GetTypeAbsByUser<T>(string auth0Id);
        Task AddDemandeAbs(DemandesDTO demande, string auth0Id);

        //Demandes GetDemandeById(int id);
    }
}
