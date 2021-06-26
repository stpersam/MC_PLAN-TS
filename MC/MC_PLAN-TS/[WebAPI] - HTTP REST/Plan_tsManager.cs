using _ClassLibraryCommon;
using System;
using System.Text.Json;

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

        internal double Login(string json)
        {
            LoginData login = JsonSerializer.Deserialize<LoginData>(json);
            return _db.VerifyUser(login.user, login.password);
        }

        internal double Login(LoginData login)
        {
            return _db.VerifyUser(login.user, login.password);
        }


        internal string UserPflanzen(string user, double sessionid)
        {
            return _db.UserPflanzen(user, sessionid);

        }

        internal string PflanzenArten(string user, double sessionid)
        {
            return _db.PflanzenArten(user, sessionid);

        }

        internal bool RegisterUser(string user, string password, string email)
        {
            return _db.RegisterUser(user, password, email);
        }

        internal string UserGruppen(string user, double sessionid)
        {
            return _db.UserGruppen(user, sessionid);
        }

        internal string PflanzeHinzufügen(string pflanze, string user, double sessionid)
        {
            throw new NotImplementedException();
        }

        internal string Initialize(string user, double sessionid)
        {
            return _db.Initialize(user, sessionid);
        }

        internal string GruppeHinzufügen(string gruppe, string user, double sessionid)
        {
            throw new NotImplementedException();
        }
    }
}
