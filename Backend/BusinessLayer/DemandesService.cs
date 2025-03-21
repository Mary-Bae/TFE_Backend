using Interfaces;
using Domain;
using Models;

namespace BusinessLayer
{
    public class DemandesService : IDemandesService
    {
        private readonly IDemandesRepository _demandeRepo;

        public DemandesService(IDemandesRepository demandesRepo)
        {
            _demandeRepo = demandesRepo;
        }
        async Task<List<T>> IDemandesService.GetDemandes<T>()
        {
            IDemandesRepository demandeRepo = _demandeRepo;
            var lst = await demandeRepo.GetDemandes<T>();
            return lst.ToList<T>();
        }
        public async Task<List<T>> GetDemandesByUser<T>(string auth0Id)
        {
            return await _demandeRepo.GetDemandesByUser<T>(auth0Id);
        }
        public async Task<List<T>> GetTypeAbsByUser<T>(string auth0Id)
        {
            return await _demandeRepo.GetTypeAbsByUser<T>(auth0Id);
        }
        public async Task AddDemandeAbs(AddDemandeDTO demande, string auth0Id)
        {
            IDemandesRepository demandeRepo = _demandeRepo;
            await demandeRepo.AddDemandeAbs(demande, auth0Id);
        }

        //public void Add(Demandes dto)
        //{
        //    _demandeRepo.Add(dto);
        //}
        //public Demandes GetDemandeById(int id)
        //{
        //    var demande = _demandeRepo.GetDemandeById(id);

        //    if (demande == null)
        //    {
        //        throw new KeyNotFoundException($"Aucune demande trouvée avec l'ID {id}");
        //    }

        //    return demande;
        //}


    }
}
