using Family.Models;
using Family.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Family.Services.Utils
{
    public interface IFamilyMapper
    {
        User ToSingleUser(UserDTO user);

        User ToSingleUser(string username, string password);

        Expression<Func<User, UserInfoDTO>> ToUserInfoDTO { get; }

        Expression<Func<Pedigree, PedigreeDTO>> ToPedigreeDTO { get; }

        Expression<Func<Person, PersonDTO>> ToPersonDTO { get; }
    }
}
