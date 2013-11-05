using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Family.Services.Models
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AuthCode { get; set; }
        public IEnumerable<PedigreeDTO> Pedigries { get; set; }
    }
}