using System;
using System.Collections.Generic;

namespace IdentityWithExistingDb.Core.Models
{
    public partial class Project
    {
        public Project()
        {
            WorkOns = new HashSet<WorkOn>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int Departmentid { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<WorkOn> WorkOns { get; set; }
    }
}
