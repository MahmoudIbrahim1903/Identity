using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithExistingDb.Core.ViewModels.Security
{
    public class RoleFormViewModel
    {
        [Required]
        [MaxLength(256)]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
