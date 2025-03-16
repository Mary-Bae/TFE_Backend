using Interfaces;
using Domain;

namespace BusinessLayer
{
    public class DemandesService : IDemandesService
    {
        private readonly IDemandesRepository _demandeRepo;

        public DemandesService(IDemandesRepository demandesRepo)
        {
            _demandeRepo = demandesRepo;
        }
        public IEnumerable<Demandes> GetDemandes()
        {
            var demande = _demandeRepo.GetDemandes();
            return demande;
        }
        public void Add(Demandes dto)
        {
            _demandeRepo.Add(dto);
        }
        public Demandes GetDemandeById(int id)
        {
            var demande = _demandeRepo.GetDemandeById(id);

            if (demande == null)
            {
                throw new KeyNotFoundException($"Aucune demande trouvée avec l'ID {id}");
            }

            return demande;
        }


    }
}
