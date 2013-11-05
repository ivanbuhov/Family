using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Family.Services.Models
{
    public class PedigreeDTO
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public IEnumerable<PersonDTO> People { get; set; }
    }
}
