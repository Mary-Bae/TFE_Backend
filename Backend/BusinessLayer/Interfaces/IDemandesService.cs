using Domain;

namespace Interfaces
{
    public interface IDemandesService
    {
        //void Add(Demandes dto);
        Task<List<T>> GetDemandes<T>();
        //Demandes GetDemandeById(int id);
    }
}
