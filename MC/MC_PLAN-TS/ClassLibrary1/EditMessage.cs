using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _ClassLibraryCommon
{

    [Serializable]
    public class EditMessage
    {
        public string actionstring { get; set; }
        public UserSessionData USD { get; set; }

    }
}
