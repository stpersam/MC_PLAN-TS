using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _ClassLibraryCommon
{
    [Serializable]
    public class RegisterUserData
    {
        public LoginData loginData { get; set; }
        public string email { get; set; }
    }
}
