using Models;

namespace Interfaces
{
    public interface IJoursFeriesService
    {
        Task<List<T>> GetJoursFeries<T>();
    }
}
