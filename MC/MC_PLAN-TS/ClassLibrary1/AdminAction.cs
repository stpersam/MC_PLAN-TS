using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _ClassLibraryCommon
{
    [Serializable]
    public class AdminAction
    {
        public LoginData loginAdmin { get; set; }
        public FullUserData userData { get; set; }
        public string action { get; set; }
    }
}
