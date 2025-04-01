using Interfaces;
using Models;

namespace BusinessLayer
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepo _managerRepo;

        public ManagerService(IManagerRepo managerRepo)
        {
            _managerRepo = managerRepo;
        }
        public async Task<T?> GetMailManagerByUser<T>(string auth0Id)
        {
            return await _managerRepo.GetMailManagerByUser<T>(auth0Id);
        }

    }
}
