using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _ClassLibrary____Common;

namespace _WebAPI____HTTP_REST
{
    public class Plan_tsManager 
    {
        private static DB_Context _db;
        public Plan_tsManager()
        {   //create DB Context Instance
            _db = new DB_Context(true);
        }


        internal string GetTestPflanzen()
        {
            return _db.GetTestPflanzen();
        }


        internal double Login(string user, string password)
        {
            return _db.VerifyUser(user, password);
        }
    }
}
