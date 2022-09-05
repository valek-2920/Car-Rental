using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Domain.Models.Entities
{
    public class UsersAll
    {
        public string Email { get; set; }
        public IList<string> Role { get; set; }
    }
}
