﻿using System;
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
        public String Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsAlive { get; set; }
        public bool IsMale { get; set; }
        [StringLength(100, ErrorMessage = "The address must be no more than 100 characteres long.")]
        public String Address { get; set; }
        [StringLength(50, ErrorMessage = "The profession must be no more than 50 characteres long.")]
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
        public virtual ICollection<Person> ChildrenFirst { get; set; }
        public virtual ICollection<Person> ChildrenSecond { get; set; }

        public Person()
        {
            this.ChildrenFirst = new HashSet<Person>();
            this.ChildrenSecond = new HashSet<Person>();
        }

        public override bool Equals(object obj)
        {
            Person other = obj as Person;
            if (other == null)
            {
                return false;
            }

            return (this.Id == other.Id);
        }
    }
}
