using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Family.Services.Models
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsAlive { get; set; }
        public bool IsMale { get; set; }
        public string Address { get; set; }
        public string Profession { get; set; }
        public int PedigreeId { get; set; }
        // Relatives:
        public int? FirstParentId { get; set; }
        public int? SecondParentId { get; set; }
        public int? SpouseId { get; set; }
    }
}
