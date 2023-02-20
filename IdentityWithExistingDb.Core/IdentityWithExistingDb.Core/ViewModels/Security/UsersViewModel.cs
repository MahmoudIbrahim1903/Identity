using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithExistingDb.Core.ViewModels.Security
{
    public class UsersViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
