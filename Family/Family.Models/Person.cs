using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Family.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public String DisplayName { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsAlive { get; set; }
        public bool IsMale { get; set; }
        public String Address { get; set; }
        public String Profession { get; set; }

        // Navigational Properties
        public int PedigreeId { get; set; }
        public virtual Pedigree Pedigree { get; set; }
    }
}
