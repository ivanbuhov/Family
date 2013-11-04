using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Data.Infrastructure
{
    public interface IFamilyUnitOfWork
    {
        IUserRepository Users { get; }
        IPedigreeRepository Pedigrees { get; }
        IPersonRepository People { get; }
        void Save();
    }
}
