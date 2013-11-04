using Family.Data;
using Family.Data.Infrastructure;
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
            IFamilyUnitOfWork data = new FamilyDbUnitOfWork();
            data.Users.Insert(new User()
            {
                Username = "malkiq sladuk bucik",
                AuthCode = "prosto404aracter4etaprosto404aracter4eta"
            });
            data.Save();
            return "value" + id;
        }
    }
}
