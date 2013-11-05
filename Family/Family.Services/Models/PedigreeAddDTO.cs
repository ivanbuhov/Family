using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Family.Services.Models
{
    public class PedigreeAddDTO
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The title must be between 3 and 30 characteres long.")]
        public string Title { get; set; }
    }
}