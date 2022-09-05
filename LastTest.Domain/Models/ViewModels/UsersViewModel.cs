using LastTest.Domain.Models.Entities;
using System.Collections.Generic;

namespace LastTest.Domain.Models.ViewModels
{
    public class UsersViewModel
    {
        public UsersViewModel()
        {
            UsersAll = new List<UsersAll>();
        }
        public IList<UsersAll> UsersAll { get; set; }
    }
}
