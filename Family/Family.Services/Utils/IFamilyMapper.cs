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

        PedigreeDTO ToSinglePedigreeDTO(Pedigree pedigree);

        UserLoggedDTO ToSingleUserLoggedDTO(User user);

        Pedigree ToSinglePedigree(PedigreeAddDTO pedigree, int ownerId = 0);

        Person ToSinglePerson(PersonInfoDTO person);

        void UpdatePedigree(PedigreeAddDTO newPedigree, Pedigree pedigreeToUpdate);

        public void UpdatePerson(PersonInfoDTO newPerson, Person personToUpdate);

        Expression<Func<User, UserInfoDTO>> ToUserInfoDTO { get; }

        Expression<Func<Pedigree, PedigreeDTO>> ToPedigreeDTO { get; }

        Expression<Func<Pedigree, PedigreeSimpleDTO>> ToPedigreeSimpleDTO { get; }

        Expression<Func<Person, PersonDTO>> ToPersonDTO { get; }

        PersonDTO ToSinglePersonDTO(Person dbPerson);
    }
}
