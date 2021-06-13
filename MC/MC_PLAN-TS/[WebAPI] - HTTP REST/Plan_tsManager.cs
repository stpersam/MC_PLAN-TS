using _ClassLibrary____Common;
using System;

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

        internal string UserPflanzen(string user, double sessionid)
        {
            return _db.UserPflanzen(user, sessionid);

        }

        internal string PflanzenArten(string user, double sessionid)
        {
            return _db.PflanzenArten(user, sessionid);

        }

        internal string UserGruppen(string user, double sessionid)
        {
            return _db.UserGruppen(user, sessionid);
        }

        internal string Initialize(string user, double sessionid)
        {
            return _db.Initialize(user, sessionid);
        }
    }
}
