using System;
using System.Collections.Generic;

namespace IdentityWithExistingDb.Core.Models
{
    public partial class Dependant
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public int EmpId { get; set; }

        public virtual Employee Emp { get; set; } = null!;
    }
}
