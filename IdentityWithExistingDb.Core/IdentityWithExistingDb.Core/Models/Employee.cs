using System;
using System.Collections.Generic;

namespace IdentityWithExistingDb.Core.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Dependants = new HashSet<Dependant>();
            WorkOns = new HashSet<WorkOn>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public int DeptId { get; set; }
        public virtual Department Dept { get; set; } = null!;
        public virtual ICollection<Dependant> Dependants { get; set; }
        public virtual ICollection<WorkOn> WorkOns { get; set; }
    }
}
