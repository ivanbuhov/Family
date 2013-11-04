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
    public class FamilyDbUnitOfWork : IFamilyUnitOfWork
    {
        private DbContext db;
        private IUserRepository users;
        private IPedigreeRepository pedigrees;
        private IPersonRepository people;

        public FamilyDbUnitOfWork()
            : this(new FamilyDbContext()) { }

        public FamilyDbUnitOfWork(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Not database context specified.");
            }

            this.db = context;
        }

        public IUserRepository Users
        {
            get 
            {
                if (this.users == null)
                {
                    this.users = new UserDbRepository(this.db);
                }
                return this.users;
            }
        }

        public IPedigreeRepository Pedigrees
        {
            get 
            {
                if (this.pedigrees == null)
                {
                    this.pedigrees = new PedigreeDbRepository(this.db);
                }
                return this.pedigrees;
            }
        }

        public IPersonRepository People
        {
            get
            {
                if (this.people == null)
                {
                    this.people = new PersonDbRepository(this.db);
                }
                return this.people;
            }
        }

        public void Save()
        {
            this.db.SaveChanges();
        }
    }
}
