using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Family.Models
{
    public class Pedigree
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength=3, ErrorMessage = "The title must be between 3 and 30 characteres long.")]
        public String Title { get; set; }

        // Navigational Properties
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Person> People { get; set; }

        public Pedigree()
        {
            this.People = new HashSet<Person>();
        }
    }
}
