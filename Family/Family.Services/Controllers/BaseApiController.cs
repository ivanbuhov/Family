using Family.Data;
using Family.Data.Infrastructure;
using Family.Models;
using Family.Services.Exceptions;
using Family.Services.Models;
using Family.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Family.Services.Controllers
{
    public class BaseApiController : ApiController
    {
        public const string UsernameHeaderName = "X-Family-Username";
        public const string AuthCodeHeaderName = "X-Family-AuthCode";

        protected IFamilyUnitOfWork data;
        protected IFamilyValidator validator;
        protected IFamilyMapper map;

        public BaseApiController()
            : this(new FamilyDbUnitOfWork(), new FamilyValidator(), new FamilyMapper()) { }

        public BaseApiController(IFamilyUnitOfWork data, IFamilyValidator validator, IFamilyMapper map)
        {
            this.data = data;
            this.validator = validator;
            this.map = map;
        }
        
        [NonAction]
        protected User Authenticate()
        {
            try
            {
                string username = Request.Headers.GetValues(UsernameHeaderName).First();
                string authCode = Request.Headers.GetValues(AuthCodeHeaderName).First();

                this.validator.ValidateUsername(username);

                User existingUser = this.data.Users.WithUsernameAndAuthCode(username, authCode);
                if (existingUser == null)
                {
                    throw new FamilyValidationException("No user exists with such an username and password.");
                }

                return existingUser;
            }
            catch (Exception)
            {
                throw new FamilyValidationException("Wrong useername or password.");
            }
        }

        [NonAction]
        protected string GetModelStateErrors(ModelStateDictionary modelState)
        {
            StringBuilder output = new StringBuilder();
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    output.AppendLine(error.ErrorMessage);
                }
            }

            return output.ToString();
        }
    }
}
