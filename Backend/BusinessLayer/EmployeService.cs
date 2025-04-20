using Interfaces;
using Models;

namespace BusinessLayer
{
    public class EmployeService : IEmployeService
    {
        private readonly IEmployeRepo _employeRepo;

        public EmployeService(IEmployeRepo employeRepo)
        {
            _employeRepo = employeRepo;
        }
        public async Task<T?> GetMailManagerByUser<T>(string auth0Id)
        {
            return await _employeRepo.GetMailManagerByUser<T>(auth0Id);
        }
        public async Task<T?> GetMailByDemande<T>(int demId)
        {
            return await _employeRepo.GetMailByDemande<T>(demId);
        }

    }
}
