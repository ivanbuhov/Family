using Family.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Family.Models;
using Family.Services.Exceptions;

namespace Family.Services.Controllers
{
    public class PeopleController : BaseApiController
    {
        [HttpPost]
        public PedigreeDTO AddParent(int id, PersonInfoDTO personInfo)
        {
            User loggedUser = this.Authenticate();
            if (!ModelState.IsValid)
            {
                string errorMessage = GetModelStateErrors(ModelState);
                throw new FamilyValidationException(errorMessage);
            }

            Person person = this.data.People.GetFull(id, loggedUser.Id);
            if(person == null) 
            {
                throw new FamilyException("No person found. Maybe the person is already deleted.");
            }

            if (person.FirstParent != null && person.SecondParent != null)
            {
                throw new FamilyException("You can't add a third parent to a person. Consider deleting or editing an existing one.");
            }

            Person dbPerson = this.map.ToSinglePerson(personInfo);
            dbPerson.PedigreeId = person.PedigreeId;
            dbPerson.Spouse = person.FirstParent ?? person.SecondParent;
            if (dbPerson.Spouse == null)
            {
                person.FirstParent = dbPerson;
            }
            else
            {
                IEnumerable<Person> children = dbPerson.Spouse.Children.ToArray();
                foreach (Person child in children)
                {
                    if (child.FirstParent == null)
                    {
                        child.FirstParent = dbPerson;
                    }
                    else if (child.SecondParent == null)
                    {
                        child.SecondParent = dbPerson;
                    }
                }
            }

            this.data.Save();
            Pedigree pedigree = this.data.Pedigrees.GetById(loggedUser.Id, dbPerson.PedigreeId);
            PedigreeDTO outputPedigree = this.map.ToSinglePedigreeDTO(pedigree);
            return outputPedigree;
        }

        [HttpPost]
        public PedigreeDTO AddChild(int id, PersonInfoDTO personInfo)
        {
            User loggedUser = this.Authenticate();
            if (!ModelState.IsValid)
            {
                string errorMessage = GetModelStateErrors(ModelState);
                throw new FamilyValidationException(errorMessage);
            }

            Person parent = this.data.People.GetFull(id, loggedUser.Id);
            if (parent == null)
            {
                throw new FamilyException("No person found. Maybe the person is already deleted.");
            }

            Person dbPerson = this.map.ToSinglePerson(personInfo);
            dbPerson.PedigreeId = parent.PedigreeId;
            dbPerson.FirstParent = parent;
            if (parent.Spouse != null)
            {
                dbPerson.SecondParent = parent.Spouse;
            }

            this.data.Save();
            Pedigree pedigree = this.data.Pedigrees.GetById(loggedUser.Id, dbPerson.PedigreeId);
            PedigreeDTO outputPedigree = this.map.ToSinglePedigreeDTO(pedigree);
            return outputPedigree;
        }

        [HttpPost]
        public PedigreeDTO AddSpouse(int id, PersonInfoDTO personInfo)
        {
            User loggedUser = this.Authenticate();
            if (!ModelState.IsValid)
            {
                string errorMessage = GetModelStateErrors(ModelState);
                throw new FamilyValidationException(errorMessage);
            }

            Person person = this.data.People.GetFull(id, loggedUser.Id);
            if (person == null)
            {
                throw new FamilyException("No person found. Maybe the person is already deleted.");
            }

            if (person.Spouse != null)
            {
                throw new FamilyException("The person already has a spouse. Consider deleting or editing the existing one.");
            }

            Person dbSpouse = this.map.ToSinglePerson(personInfo);
            dbSpouse.PedigreeId = person.PedigreeId;
            dbSpouse.Spouse = person;
            person.Spouse = dbSpouse;

            foreach (Person child in person.Children)
            {
                if (child.FirstParent == null)
                {
                    child.FirstParent = dbSpouse;
                }
                else if (child.SecondParent == null)
                {
                    child.SecondParent = dbSpouse;
                }
            }

            this.data.Save();
            Pedigree pedigree = this.data.Pedigrees.GetById(loggedUser.Id, dbSpouse.PedigreeId);
            PedigreeDTO outputPedigree = this.map.ToSinglePedigreeDTO(pedigree);
            return outputPedigree;
        }

        [HttpDelete]
        public PedigreeDTO Delete(int id)
        {
            User loggedUser = this.Authenticate();

            Person person = this.data.People.GetFull(id, loggedUser.Id);
            if (person == null)
            {
                throw new FamilyException("No person found. Maybe the person is already deleted.");
            }

            if (person.Spouse == null && person.Children.Count() > 0)
            {
                throw new FamilyException("The person you are trying to delete has no spouse but has children. Such a person can't be deleted.");
            }

            if (person.Spouse != null)
            {
                person.Spouse.Spouse = null;
            }

            foreach (Person child in person.Children)
            {
                if (child.FirstParentId == person.Id)
                {
                    child.FirstParentId = null;
                }
                else if (child.SecondParentId == person.Id)
                {
                    child.SecondParentId = null;
                }
            }

            this.data.Save();
            Pedigree pedigree = this.data.Pedigrees.GetById(loggedUser.Id, person.PedigreeId);
            PedigreeDTO outputPedigree = this.map.ToSinglePedigreeDTO(pedigree);
            return outputPedigree;
        }

        [HttpPut]
        public PedigreeDTO Update(int id, PersonInfoDTO personInfo)
        {
            User loggedUser = this.Authenticate();
            if (!ModelState.IsValid)
            {
                string errorMessage = GetModelStateErrors(ModelState);
                throw new FamilyValidationException(errorMessage);
            }

            Person dbPerson = this.data.People.GetById(id);
            if (dbPerson == null)
            {
                throw new FamilyException("No person found. Maybe the person is already deleted.");
            }

            this.map.UpdatePerson(personInfo, dbPerson);

            this.data.Save();
            Pedigree pedigree = this.data.Pedigrees.GetById(loggedUser.Id, dbPerson.PedigreeId);
            PedigreeDTO outputPedigree = this.map.ToSinglePedigreeDTO(pedigree);
            return outputPedigree;
        }
    }
}
