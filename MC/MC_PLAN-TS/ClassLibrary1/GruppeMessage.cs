using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _ClassLibraryCommon
{

    [Serializable]
    public class GruppeMessage
    {
        public Gruppe gruppe { get; set; }
        public UserSessionData usd { get; set; }

    }
}
