using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Family.Services.Exceptions
{
    public class FamilyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public const string DefaultDisplayMessage = "An error occured while proccessing the operation. Please try again.";
        public override void OnException(HttpActionExecutedContext context)
        {
            string displayMessage = DefaultDisplayMessage;
            if (context.Exception is FamilyException)
            {
                displayMessage = context.Exception.Message;
            }

            context.Response = context.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new { DisplayMessage = displayMessage },
                    "application/json");
        }
    }
}