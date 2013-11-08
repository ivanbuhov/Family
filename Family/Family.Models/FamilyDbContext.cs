using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Models
{
    public class FamilyDbContext : DbContext
    {
        public FamilyDbContext()
            : base("name=FamilyDbContext") { }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Pedigree> Pedigrees { get; set; }
        public IDbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Removes one-to-many cascade deletion
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Mother and Father relationships settings
            modelBuilder.Entity<Person>().HasMany(p => p.Children).WithOptional().HasForeignKey(p => p.FirstParentId);
            modelBuilder.Entity<Person>().HasMany(p => p.Children).WithOptional().HasForeignKey(p => p.SecondParentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
