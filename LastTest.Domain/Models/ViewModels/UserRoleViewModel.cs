using LastTest.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Domain.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            Users = new List<UserRole>();
        }
        public string Role { get; set; }

        public IList<UserRole> Users { get; set; }
    }
}
