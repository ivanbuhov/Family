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
            return this.ToSingleUser(user.Username, user.Password);
        }

        public User ToSingleUser(string username, string password)
        {
            return new User
            {
                Username = username,
                AuthCode = sha1(username + sha1(password))
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
                    MotherId = person.MotherId,
                    FatherId = person.FatherId,
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
                    People = pedigree.People.AsQueryable().Select(this.ToPersonDTO)
                };
            }
        }
    }
}