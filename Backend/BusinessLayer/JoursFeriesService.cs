using Interfaces;
using Models;

namespace BusinessLayer
{
    public class JoursFeriesService : IJoursFeriesService
    {
        private readonly IJoursFeriesRepo _joursFeriesRepo;

        public JoursFeriesService(IJoursFeriesRepo joursFeriesRepo)
        {
            _joursFeriesRepo = joursFeriesRepo;
        }
        public async Task<List<T>> GetJoursFeries<T>()
        {
            return await _joursFeriesRepo.GetJoursFeries<T>();
        }
    }
}
