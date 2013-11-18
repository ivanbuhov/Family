using Family.Models;
using Family.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Family.Services.Utils
{
    public class FamilyMapper : IFamilyMapper
    {
        private static string sha1(string text)
        {
            var sha1 = SHA1.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = sha1.ComputeHash(bytes);

            // Convert to string
            var output = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                string hex = b.ToString("x2");
                output.Append(hex);
            }
            return output.ToString();
        }

        public User ToSingleUser(UserDTO user)
        {
            string authCode = sha1(user.Username + sha1(user.Password));
            return this.ToSingleUser(user.Username, authCode);
        }

        public User ToSingleUser(string username, string authCode)
        {
            return new User
            {
                Username = username,
                AuthCode = authCode
            };
        }

        public UserLoggedDTO ToSingleUserLoggedDTO(User user)
        {
            return new UserLoggedDTO
            {
                Id = user.Id,
                Username = user.Username,
                AuthCode = user.AuthCode
            };
        }

        public PedigreeDTO ToSinglePedigreeDTO(Pedigree pedigree)
        {
            return new PedigreeDTO
            {
                Id = pedigree.Id,
                Title = pedigree.Title,
                OwnerId = pedigree.OwnerId,
                People = pedigree.People.AsQueryable().Select(this.ToPersonDTO)
            };
        }

        public Expression<Func<User, UserInfoDTO>> ToUserInfoDTO
        {
            get 
            {
                return user => new UserInfoDTO()
                {
                    Id = user.Id,
                    Username = user.Username,
                    AuthCode = user.AuthCode,
                    Pedigries = user.Pedigrees.AsQueryable().Select(this.ToPedigreeDTO)
                };
            }
        }

        public Expression<Func<Person, PersonDTO>> ToPersonDTO {
            get
            {
                return person => new PersonDTO()
                {
                    Id = person.Id,
                    DisplayName = person.DisplayName,
                    FirstName = person.FirstName,
                    MiddleName = person.MiddleName,
                    LastName = person.LastName,
                    Nickname = person.Nickname,
                    Email = person.Email,
                    BirthDate = person.BirthDate,
                    IsAlive = person.IsAlive,
                    IsMale = person.IsMale,
                    Address = person.Address,
                    Profession = person.Profession,
                    PedigreeId = person.PedigreeId,
                    FirstParentId = person.FirstParentId,
                    SecondParentId = person.SecondParentId,
                    SpouseId = person.SpouseId
                };
            }
        }

        public Expression<Func<Pedigree, PedigreeDTO>> ToPedigreeDTO
        {
            get 
            {
                return pedigree => new PedigreeDTO
                {
                    Id = pedigree.Id,
                    Title = pedigree.Title,
                    OwnerId = pedigree.OwnerId,
                    People = pedigree.People.AsQueryable().Select(this.ToPersonDTO)
                };
            }
        }

        public Expression<Func<Pedigree, PedigreeSimpleDTO>> ToPedigreeSimpleDTO
        {
            get
            {
                return pedigree => new PedigreeSimpleDTO
                {
                    Id = pedigree.Id,
                    OwnerId = pedigree.OwnerId,
                    Title = pedigree.Title
                };
            }
        }

        public void UpdatePedigree(PedigreeAddDTO newPedigree, Pedigree pedigreeToUpdate)
        {
            pedigreeToUpdate.Title = newPedigree.Title;
        }

        public void UpdatePerson(PersonInfoDTO newPerson, Person personToUpdate)
        {
            personToUpdate.DisplayName = newPerson.DisplayName;
            personToUpdate.FirstName = newPerson.FirstName;
            personToUpdate.MiddleName = newPerson.MiddleName;
            personToUpdate.LastName = newPerson.LastName;
            personToUpdate.Nickname = newPerson.Nickname;
            personToUpdate.Email = newPerson.Email;
            personToUpdate.BirthDate = newPerson.BirthDate;
            personToUpdate.IsAlive = newPerson.IsAlive;
            personToUpdate.IsMale = newPerson.IsMale;
            personToUpdate.Address = newPerson.Address;
            personToUpdate.Profession = newPerson.Profession;
        }

        public Pedigree ToSinglePedigree(PedigreeAddDTO pedigree, int ownerId = 0)
        {
            return new Pedigree
            {
                Id = 0,
                Title = pedigree.Title,
                OwnerId = ownerId
            };
        }

        public Person ToSinglePerson(PersonInfoDTO person)
        {
            return new Person()
            {
                Id = 0,
                DisplayName = person.DisplayName,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                Nickname = person.Nickname,
                Email = person.Email,
                BirthDate = person.BirthDate,
                IsAlive = person.IsAlive,
                IsMale = person.IsMale,
                Address = person.Address,
                Profession = person.Profession,
                Pedigree = null,
                FirstParent = null,
                SecondParent = null,
                SpouseId = null
            };
        }

        public PersonDTO ToSinglePersonDTO(Person person)
        {
            return new PersonDTO()
            {
                Id = person.Id,
                DisplayName = person.DisplayName,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                Nickname = person.Nickname,
                Email = person.Email,
                BirthDate = person.BirthDate,
                IsAlive = person.IsAlive,
                IsMale = person.IsMale,
                Address = person.Address,
                Profession = person.Profession,
                PedigreeId = person.PedigreeId,
                FirstParentId = person.FirstParentId,
                SecondParentId = person.SecondParentId,
                SpouseId = person.SpouseId
            };
        }
    }
}