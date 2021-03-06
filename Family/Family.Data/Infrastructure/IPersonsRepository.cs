﻿using Family.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Data.Infrastructure
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person GetFull(int personId, int ownerId);

        Person GetById(int id);
    }
}
