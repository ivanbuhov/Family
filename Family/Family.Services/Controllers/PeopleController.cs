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

            Person child = this.data.People.GetFull(id, loggedUser.Id);
            if(child == null) 
            {
                throw new FamilyException("No person found. Maybe the person is already deleted.");
            }

            if (child.FirstParent != null && child.SecondParent != null)
            {
                throw new FamilyException("You can't add a third parent to a person. Consider deleting or editing an existing one.");
            }

            Person parentToAdd = this.map.ToSinglePerson(personInfo);
            parentToAdd.PedigreeId = child.PedigreeId;
            parentToAdd.Spouse = child.FirstParent ?? child.SecondParent;
            if (parentToAdd.Spouse == null)
            {
                child.FirstParent = parentToAdd;
            }
            else
            {
                parentToAdd.Spouse.Spouse = parentToAdd;
                IEnumerable<Person> childrenFirst = parentToAdd.Spouse.ChildrenFirst.ToArray();
                IEnumerable<Person> childrenSecond = parentToAdd.Spouse.ChildrenSecond.ToArray();
                IEnumerable<Person> children = childrenFirst.Union(childrenSecond);
                foreach (Person singleChild in children)
                {
                    if (singleChild.FirstParent == null)
                    {
                        singleChild.FirstParent = parentToAdd;
                    }
                    else if (singleChild.SecondParent == null)
                    {
                        singleChild.SecondParent = parentToAdd;
                    }
                }
            }

            this.data.Save();
            Pedigree pedigree = this.data.Pedigrees.GetById(loggedUser.Id, parentToAdd.PedigreeId);
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
            parent.ChildrenFirst.Add(dbPerson);
            if (parent.Spouse != null)
            {
                parent.Spouse.ChildrenSecond.Add(dbPerson);
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
            // Addifng spouse relashionship
            dbSpouse.Spouse = person;
            person.Spouse = dbSpouse;
            // Adding child -> parent relashionship
            dbSpouse.ChildrenFirst = person.ChildrenSecond;
            dbSpouse.ChildrenSecond = person.ChildrenFirst;

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

            IEnumerable<Person> personChildren = person.ChildrenFirst.Union(person.ChildrenSecond);
            if (person.Spouse == null && personChildren.Count() > 0)
            {
                throw new FamilyException(String.Format(
                    "{0} has no spouse but has children. Such a person can't be deleted. Consider deleting his/her children first.", person.DisplayName));
            }

            if (person.Spouse != null)
            {
                person.Spouse.Spouse = null;
            }

            foreach (Person child in personChildren)
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

            this.data.People.Delete(person);
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
