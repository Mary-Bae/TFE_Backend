using Models;

namespace Interfaces
{
    public interface ICompteurService
    {
        Task<List<T>> GetCompteurByUser<T>(string auth0Id);
    }
}
