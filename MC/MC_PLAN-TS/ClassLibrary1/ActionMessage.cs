﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _ClassLibraryCommon
{
    [Serializable]
    public class ActionMessage
    {
        public string @string { get; set; }
        public LoginData loginData { get; set; }
        
    }
}
