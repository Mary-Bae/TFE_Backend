using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDemandesRepository
    {
        void Add(Demandes demande);
        IEnumerable<Demandes> GetDemandes();
        Demandes GetDemandeById(int id);
    }
}
