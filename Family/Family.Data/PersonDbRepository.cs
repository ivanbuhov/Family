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

        public Person GetFull(int personId, int ownerId)
        {
            return this.db.Set<Person>()
                .Include("FirstParent")
                .Include("SecondParent")
                .Include("Spouse")
                .Include("Children")
                .FirstOrDefault(p => p.Id == personId && p.Pedigree.OwnerId == ownerId);
        }


        public Person GetById(int id)
        {
            return this.db.Set<Person>().FirstOrDefault(p => p.Id == id);
        }
    }
}
