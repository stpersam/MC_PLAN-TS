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


        //Login
        internal int Login(LoginData login)
        {
            return _db.VerifyUser(login.user, login.password);
        }

        //GET DATA
        internal string Initialize(UserSessionData usd)
        {
            return _db.Initialize(usd.user, usd.sessionid);
        }

        internal string GetTestPflanzen()
        {
            return _db.GetTestPflanzen();
        }
        internal string UserPflanzen(UserSessionData usd)
        {
            return _db.UserPflanzen(usd.user, usd.sessionid);

        }

        internal string PflanzenArten(UserSessionData usd)
        {
            return _db.PflanzenArten(usd.user, usd.sessionid);

        }
        internal string GetUsers(UserSessionData USD)
        {
            return _db.GetUsers(USD.user, USD.sessionid);
        }

        internal string UserGruppen(UserSessionData usd)
        {
            return _db.UserGruppen(usd.user, usd.sessionid);
        }

      
        //UserActions
        internal bool RegisterUser(FullUserData rud)
        {
            return _db.RegisterUser(rud.loginData.user, rud.loginData.password, rud.email);
        }

        internal bool ChangePassword(ActionMessage action)
        {
            return _db.ChangePassword(action.loginData.user, action.loginData.password, action.actionstring);
        }
        internal bool AdminEditUser(AdminAction action)
        {
            return _db.AdminEditUser(action.USDAdmin, action.userData, action.action);
        }

        internal bool DeleteUser(ActionMessage action)
        {
            return _db.DeleteUser(action.loginData.user, action.loginData.password, action.actionstring);
        }


        //Add, Delete, Modify Plants/Groups           
        /*
        internal bool PflanzeHinzufügen(EditMessage action)
        {
            return _db.PflanzeHinzufügen(action.usd, action.actionstring);
        }
        */
        internal bool PflanzeHinzufügen(PflanzeMessage pmsg)
        {
            return _db.PflanzeHinzufügen(pmsg);
        }

        internal bool GruppeHinzufügen(EditMessage action)
        {
            return _db.GruppeHinzufügen(action.usd, action.actionstring);
        }

        internal bool PflanzeLöschen(EditMessage action)
        {
            return _db.PflanzeLöschen(action.usd, action.actionstring);
        }

        internal bool GruppeLöschen(EditMessage action)
        {
            return _db.GruppeLöschen(action.usd, action.actionstring);
        }

        internal bool PflanzeBearbeiten(EditMessage action)
        {
            return _db.PflanzeBearbeiten(action.usd, action.actionstring);
        }

        internal bool GruppeBearbeiten(EditMessage action)
        {
            return _db.GruppeBearbeiten(action.usd, action.actionstring);
        }
    }
}
