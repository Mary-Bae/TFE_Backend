using Interfaces;
using Models;

namespace BusinessLayer
{
    public class CompteurService : ICompteurService
    {
        private readonly ICompteurRepo _compteurRepo;

        public CompteurService(ICompteurRepo compteurRepo)
        {
            _compteurRepo = compteurRepo;
        }
        public async Task<List<T>> GetCompteurByUser<T>(string auth0Id)
        {
            return await _compteurRepo.GetCompteurByUser<T>(auth0Id);
        }
    }
}
