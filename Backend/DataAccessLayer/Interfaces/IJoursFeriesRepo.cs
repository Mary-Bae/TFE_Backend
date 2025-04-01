
namespace Interfaces
{
    public interface IJoursFeriesRepo
    {
        Task<List<T>> GetJoursFeries<T>();
    }
}
