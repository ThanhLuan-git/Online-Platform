using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class AuthRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}