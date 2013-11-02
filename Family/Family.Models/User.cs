using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Models
{
    public class User
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String AuthCode { get; set; }  // AuthCode = Sha1(Username + Sha1(Password))

        // Navigational Properties
        public virtual ICollection<Pedigree> Pedigrees { get; set; }

        public User()
        {
            this.Pedigrees = new HashSet<Pedigree>();
        }
    }
}
