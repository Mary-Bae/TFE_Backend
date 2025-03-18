using Domain;

namespace Interfaces
{
    public interface IDemandesRepository
    {
        //void Add(Demandes demande);
        Task<List<T>> GetDemandes<T>();
        //Demandes GetDemandeById(int id);
    }
}
