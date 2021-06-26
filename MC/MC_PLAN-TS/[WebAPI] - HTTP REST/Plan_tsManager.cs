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

        internal double Login(LoginData login)
        {
            return _db.VerifyUser(login.user, login.password);
        }


        internal string UserPflanzen(UserSessionData usd)
        {
            return _db.UserPflanzen(usd.user, usd.sessionid);

        }

        internal string PflanzenArten(UserSessionData usd)
        {
            return _db.PflanzenArten(usd.user, usd.sessionid);

        }

        internal bool RegisterUser(RegisterUserData rud)
        {
            return _db.RegisterUser(rud.loginData.user, rud.loginData.password, rud.email);
        }

        internal string UserGruppen(UserSessionData usd)
        {
            return _db.UserGruppen(usd.user, usd.sessionid);
        }

        internal string Initialize(UserSessionData usd)
        {
            return _db.Initialize(usd.user, usd.sessionid);
        }

        internal string PflanzeHinzufügen(ActionMessage action)
        {
            throw new NotImplementedException();
        }

        internal string GruppeHinzufügen(ActionMessage action)
        {
            throw new NotImplementedException();
        }

        

    }
}
