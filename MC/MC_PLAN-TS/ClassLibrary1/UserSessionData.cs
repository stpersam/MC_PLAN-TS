﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _ClassLibraryCommon
{
    [Serializable]
    public class UserSessionData
    {
        public string user { get; set; }
        public int sessionid { get; set; }
    }
}
