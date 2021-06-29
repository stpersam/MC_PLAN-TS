using _ClassLibraryCommon;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace _WebAPI____HTTP_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Plan_tsController : Controller
    {
        private static Plan_tsManager _manager;

        static Plan_tsController()
        {
            _manager = new Plan_tsManager();
        }

        //Login
        [HttpPost()]
        [Route("Login")]
        public int Login([FromBody] LoginData loginData) => _manager.Login(loginData);

        //GET DATA
        [HttpPost()]
        [Route("Initialize")]
        public string Initialize([FromBody] UserSessionData userSessionData) => _manager.Initialize(userSessionData);
        [HttpGet]
        [Route("GetTestPflanzen")]
        public string GetTestPflanzen() => _manager.GetTestPflanzen();        
        [HttpPost()]
        [Route("GetUsers")]
        public string GetUsers([FromBody] UserSessionData usd) => _manager.GetUsers(usd);        
        [HttpPost()]
        [Route("GetUserPflanzen")]
        public string UserPflanzen([FromBody] UserSessionData userSessionData) => _manager.UserPflanzen(userSessionData);
        [HttpPost()]
        [Route("GetUserGruppen")]
        public string UserGruppen([FromBody] UserSessionData userSessionData) => _manager.UserGruppen(userSessionData);
        [HttpPost()]
        [Route("GetPflanzenArten")]
        public string PflanzenArten([FromBody] UserSessionData userSessionData) => _manager.PflanzenArten(userSessionData);

        //UserActions
        [HttpPost()]
        [Route("RegisterUser")]
        public bool RegisterUser([FromBody] FullUserData registerUserData) => _manager.RegisterUser(registerUserData);
        [HttpPost()]
        [Route("ChangePassword")]
        public bool ChangePassword([FromBody] ActionMessage action) => _manager.ChangePassword(action);
        [HttpPost()]
        [Route("DeleteUser")]
        public bool DeleteUser([FromBody] ActionMessage action) => _manager.DeleteUser(action);
        [HttpPost()]
        [Route("AdminEditUser")]
        public bool AdminEditUser([FromBody] AdminAction action) => _manager.AdminEditUser(action);

        //Add, Delete, Modify Plants/Groups
        [HttpPost()]
        [Route("AddPflanze")]
        public bool PflanzeHinzufügen([FromBody] EditMessage action) => _manager.PflanzeHinzufügen(action);
        [HttpPost()]
        [Route("AddGruppe")]
        public bool GruppeHinzufügen([FromBody] EditMessage action) => _manager.GruppeHinzufügen(action);
        [HttpPost()]
        [Route("DeletePflanze")]
        public bool PflanzeLöschen([FromBody] EditMessage action) => _manager.PflanzeLöschen(action);
        [HttpPost()]
        [Route("DeleteGruppe")]
        public bool GruppeLöschen([FromBody] EditMessage action) => _manager.GruppeLöschen(action);
        [HttpPost()]
        [Route("EditPflanze")]
        public bool PflanzeBearbeiten([FromBody] EditMessage action) => _manager.PflanzeBearbeiten(action);
        [HttpPost()]
        [Route("EditGruppe")]
        public bool GruppeBearbeiten([FromBody] EditMessage action) => _manager.GruppeBearbeiten(action);

      
    }
}
