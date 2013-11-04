using Family.Data.Infrastructure;
using Family.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Data
{
    public class PersonDbRepository : BaseDbRepository<Person>, IPersonRepository
    {
        public PersonDbRepository(DbContext context)
            : base(context) { }
    }
}
