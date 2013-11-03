using Family.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Family.Services.Controllers
{
    public class TestController : ApiController
    {
        public String GetTest(int id)
        {
            return "value" + id;
        }
    }
}
