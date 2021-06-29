using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _ClassLibraryCommon
{

    [Serializable]
    public class PflanzeMessage
    {
        public Pflanze pflanze { get; set; }
        public UserSessionData usd { get; set; }

    }
}
