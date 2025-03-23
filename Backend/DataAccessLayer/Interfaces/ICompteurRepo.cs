using Models;

namespace Interfaces
{
    public interface ICompteurRepo
    {
        Task<List<T>> GetCompteurByUser<T>(string auth0Id);

    }
}
