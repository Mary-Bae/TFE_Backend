using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Interfaces
{
    public interface IDemandesService
    {
        void Add(Demandes demande);
        IEnumerable<Demandes> GetDemandes();
        Demandes GetDemandeById(int id);
    }
}
