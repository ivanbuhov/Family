using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Family.Models
{
    public class Pedigree
    {
        public int Id { get; set; }
        public String Title { get; set; }

        // Navigational Properties
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public int CentralPersonId { get; set; }
        public virtual Person CentralPerson { get; set; }

        public Pedigree()
        {
            this.People = new HashSet<Person>();
        }
    }
}
