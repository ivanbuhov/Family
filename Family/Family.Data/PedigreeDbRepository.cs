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
    public class PedigreeDbRepository : BaseDbRepository<Pedigree>, IPedigreeRepository
    {
        public PedigreeDbRepository(DbContext context)
            : base(context) { }

        public override void Delete(Pedigree entity)
        {
            this.db.Set<Person>().RemoveRange(entity.People);
            this.db.Set<Pedigree>().Remove(entity);
        }

        public IEnumerable<Pedigree> Get(int ownerId)
        {
            return this.Get(p => p.OwnerId == ownerId);
        }

        public Pedigree GetById(int ownerId, int id, bool includePeople = true)
        {
            IQueryable<Pedigree> pedigree = this.db.Set<Pedigree>();
            if (includePeople)
            {
                pedigree = pedigree.Include(p => p.People);
            }
            return pedigree.FirstOrDefault(p => p.Id == id && p.OwnerId == ownerId);
        }
    }
}
