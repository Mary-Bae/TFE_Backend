using Interfaces;
using Models;

namespace BusinessLayer
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepo _roleRepo;

        public RoleService(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }
        public async Task<List<T>> GetRoles<T>()
        {
            return await _roleRepo.GetRoles<T>();
        }
    }
}
