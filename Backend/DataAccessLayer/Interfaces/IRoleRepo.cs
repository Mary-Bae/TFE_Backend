using Models;

namespace Interfaces
{
    public interface IRoleRepo
    {
        Task<List<T>> GetRoles<T>();
    }
}
