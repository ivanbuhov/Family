using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Models
{
    public class FamilyDbContext : DbContext
    {
        public IDbSet<User> Users { get; set; }
        public IDbSet<Pedigree> Pedigrees { get; set; }
        public IDbSet<Person> Persons { get; set; }
    }
}
