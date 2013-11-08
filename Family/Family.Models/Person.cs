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
        [StringLength(30, ErrorMessage = "The display name must be no more than 30 characteres long.")]
        public String DisplayName { get; set; }
        [StringLength(30, ErrorMessage="The first name must be no more than 30 characteres long.")]
        public String FirstName { get; set; }
        [StringLength(30, ErrorMessage="The middle name must be no more than 30 characteres long.")]
        public String MiddleName { get; set; }

        [StringLength(30, ErrorMessage="The last name must be no more than 30 characteres long.")]
        public String LastName { get; set; }

        [StringLength(30, ErrorMessage="The nickname must be no more than 30 characteres long.")]
        public String Nickname { get; set; }

        [StringLength(50, ErrorMessage="The email must be no more than 50 characteres long.")]
        [EmailAddress(ErrorMessage="Invalid email. Please check it again.")]
        public String Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsAlive { get; set; }
        public bool IsMale { get; set; }
        public String Address { get; set; }
        public String Profession { get; set; }

        // Navigational Properties
        public int PedigreeId { get; set; }
        public virtual Pedigree Pedigree { get; set; }
        // Relatives:
        public int? FirstParentId { get; set; }
        public virtual Person FirstParent { get; set; }
        public int? SecondParentId { get; set; }
        public virtual Person SecondParent { get; set; }
        public int? SpouseId { get; set; }
        public virtual Person Spouse { get; set; }
        public virtual ICollection<Person> Children { get; set; }

        public Person()
        {
            this.Children = new HashSet<Person>();
        }
    }
}
