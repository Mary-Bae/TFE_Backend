using Domain;

namespace Interfaces
{
    public interface IDemandesService
    {
        //void Add(Demandes dto);
        Task<List<T>> GetDemandes<T>();
        Task<List<T>> GetDemandesByUser<T>(string auth0Id);
        //Demandes GetDemandeById(int id);
    }
}
