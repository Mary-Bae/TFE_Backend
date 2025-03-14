using Interfaces;
using Domain;

namespace DataAccessLayer
{
    public class DemandesRepository : IDemandesRepository
    {

        //Provisoire en attente de DB
        private static List<Demandes> _dbProvisoire = new List<Demandes> {new Demandes("Congé légal"), new Demandes("Congé de maternité"), new Demandes("congé de circonstances") };
       
        public IEnumerable<Demandes> GetDemandes()
        {
            return _dbProvisoire;
        }
        public void Add(Demandes dto)
        {
            var demande = new Demandes(dto.Type)
            {
                Id = _dbProvisoire.Count + 1,
                Type = dto.Type,
                DateBegin = dto.DateBegin,
                DateEnd = dto.DateEnd,
                Comment = dto.Comment
            };
            _dbProvisoire.Add(demande);
        }

    }
}
