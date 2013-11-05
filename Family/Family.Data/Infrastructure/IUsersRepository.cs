using Family.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Data.Infrastructure
{
    public interface IUserRepository : IRepository<User>
    {
        User WithUsername(string username);
        User WithUsernameAndAuthCode(string username, string authCode);
        User GetAllInfoOf(int userId);
    }
}
