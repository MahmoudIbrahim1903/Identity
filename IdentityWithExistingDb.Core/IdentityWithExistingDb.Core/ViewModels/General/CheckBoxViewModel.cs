using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithExistingDb.Core.ViewModels.General
{
    public class CheckBoxViewModel
    {
        public string Value { get; set; }

        public string DisplayName { get; set; }

        public bool IsChecked { get; set; }
    }
}
