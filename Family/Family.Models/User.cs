using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "The username must be between 5 than 30 characteres long.")]
        public String Username { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 40, ErrorMessage = "The authentication code must be 40 characteres long.")]
        public String AuthCode { get; set; }  // AuthCode = Sha1(Username + Sha1(Password))

        // Navigational Properties
        public virtual ICollection<Pedigree> Pedigrees { get; set; }

        public User()
        {
            this.Pedigrees = new HashSet<Pedigree>();
        }
    }
}
