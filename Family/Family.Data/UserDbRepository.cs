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
    public class UserDbRepository : BaseDbRepository<User>, IUserRepository
    {
        public UserDbRepository(DbContext context)
            : base(context) { }

        public User WithUsername(string username)
        {
            return this.FirstOrDefault(user => user.Username == username);
        }

        public User WithUsernameAndAuthCode(string username, string authCode)
        {
            return this.FirstOrDefault(user => user.Username == username && user.AuthCode == authCode);
        }

        public User GetAllInfoOf(int userId)
        {
            return this.Get(user => user.Id == userId)
                .Include("Pedigrees")
                .Include("Pedigrees.People")
                .First();
        }
    }
}
