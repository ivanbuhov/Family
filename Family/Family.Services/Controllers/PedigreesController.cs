using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Family.Models;
using Family.Services.Exceptions;
using Family.Services.Models;

namespace Family.Services.Controllers
{
    public class PedigreesController : BaseApiController
    {
        // GET api/Pedigrees
        [HttpGet]
        public IEnumerable<PedigreeSimpleDTO> GetPedigrees()
        {
            User loggedUser = this.Authenticate();
            IEnumerable<PedigreeSimpleDTO> pedigrees = this.data.Pedigrees.Get(loggedUser.Id).AsQueryable().Select(this.map.ToPedigreeSimpleDTO);
            return pedigrees;
        }

        // GET api/Pedigrees/5
        [HttpGet]
        [ResponseType(typeof(Pedigree))]
        public IHttpActionResult GetPedigree(int id)
        {
            User loggedUser = this.Authenticate();
            Pedigree pedigree = this.data.Pedigrees.GetById(loggedUser.Id, id);
            if (pedigree == null)
            {
                throw new FamilyException("The pedigree doesn't exists. Maybe it was deleted.");
            }

            PedigreeDTO outputPedigree = this.map.ToSinglePedigreeDTO(pedigree);
            return Ok(outputPedigree);
        }

        // POST api/Pedigrees
        [HttpPost]
        [ResponseType(typeof(Pedigree))]
        public IHttpActionResult AddPedigree(PedigreeAddDTO pedigree)
        {
            User loggedUser = this.Authenticate();
            if (!ModelState.IsValid)
            {
                string errorMessage = GetModelStateErrors(ModelState);
                throw new FamilyValidationException(errorMessage);
            }

            Pedigree dbPedigree = this.map.ToSinglePedigree(pedigree, loggedUser.Id);
            this.data.Pedigrees.Insert(dbPedigree);
            this.data.Save();

            IEnumerable<PedigreeSimpleDTO> pedigrees = this.data.Pedigrees.Get(loggedUser.Id).AsQueryable().Select(this.map.ToPedigreeSimpleDTO);
            return CreatedAtRoute("DefaultApi", new { id = dbPedigree.Id }, pedigrees);
        }

        // PUT api/Pedigrees/5
        [HttpPut]
        public IEnumerable<PedigreeSimpleDTO> UpdatePedigree(int id, PedigreeAddDTO pedigree)
        {
            User loggedUser = this.Authenticate();

            Pedigree dbPedigree = this.data.Pedigrees.GetById(loggedUser.Id, id, false);
            if (dbPedigree == null)
            {
                throw new FamilyException("The pedigree you are trying to update doesn't exists. Maybe it is deleted.");
            }
            if (!ModelState.IsValid)
            {
                string errorMessage = GetModelStateErrors(ModelState);
                throw new FamilyValidationException(errorMessage);
            }

            this.map.UpdatePedigree(pedigree, dbPedigree);
            this.data.Pedigrees.Update(dbPedigree);
            this.data.Save();

            IEnumerable<PedigreeSimpleDTO> pedigrees = this.data.Pedigrees.Get(loggedUser.Id).AsQueryable().Select(this.map.ToPedigreeSimpleDTO);
            return pedigrees;
        }

        // DELETE api/Pedigrees/5
        [HttpDelete]
        [ResponseType(typeof(Pedigree))]
        public IHttpActionResult DeletePedigree(int id)
        {
            User loggedUser = this.Authenticate();
            Pedigree pedigree = this.data.Pedigrees.GetById(loggedUser.Id, id);
            if (pedigree == null)
            {
                throw new FamilyException("The pedigree you are trying to delete doesn't exists. Maybe it was deleted.");
            }

            this.data.Pedigrees.Delete(pedigree);
            this.data.Save();
            IEnumerable<PedigreeSimpleDTO> pedigrees = this.data.Pedigrees.Get(loggedUser.Id).AsQueryable().Select(this.map.ToPedigreeSimpleDTO);

            return Ok(pedigrees);
        }
    }
}