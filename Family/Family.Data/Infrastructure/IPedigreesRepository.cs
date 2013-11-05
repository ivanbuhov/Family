using Family.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Data.Infrastructure
{
    public interface IPedigreeRepository : IRepository<Pedigree>
    {
        IEnumerable<Pedigree> Get(int ownerId);

        Pedigree GetById(int ownerId, int id, bool includePeople = true);
    }
}
