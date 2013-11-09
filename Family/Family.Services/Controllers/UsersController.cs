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
using Family.Services.Models;
using Family.Services.Exceptions;
using System.Web.Http.ValueProviders;

namespace Family.Services.Controllers
{
    public class UsersController : BaseApiController
    {
        [HttpPost]
        public HttpResponseMessage Register(UserDTO user)
        {
            this.validator.ValidateUsername(user.Username);
            this.validator.ValidatePassword(user.Password);

            User existingUser = this.data.Users.WithUsername(user.Username);
            if (existingUser != null)
            {
                throw new FamilyValidationException(string.Format("This username {0} already exists.", user.Username));
            }
            User dbUser = this.map.ToSingleUser(user);
            this.data.Users.Insert(dbUser);
            this.data.Save();

            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created, dbUser);
            return response;
        }

        [HttpGet]
        public HttpResponseMessage Info()
        {
            User loggedUser = this.Authenticate();
            User userInfo = this.data.Users.GetAllInfoOf(loggedUser.Id);

            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK, loggedUser);
            return response;
        }
    }
}