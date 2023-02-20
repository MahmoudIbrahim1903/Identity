using System;
using System.Collections.Generic;

namespace IdentityWithExistingDb.Core.Models
{
    public partial class WorkOn
    {
        public int Eid { get; set; }
        public int Pid { get; set; }
        public int Hours { get; set; }

        public virtual Employee EidNavigation { get; set; } = null!;
        public virtual Project PidNavigation { get; set; } = null!;
    }
}
