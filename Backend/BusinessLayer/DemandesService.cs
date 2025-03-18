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
