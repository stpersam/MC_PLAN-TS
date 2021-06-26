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

        [HttpGet]
        [Route("GetTestPflanzen")]
        public string GetTestPflanzen() => _manager.GetTestPflanzen();
        [HttpPost()]
        [Route("RegisterUser")]
        public bool RegisterUser([FromBody]RegisterUserData registerUserData) => _manager.RegisterUser(registerUserData);

        [HttpPost()]
        [Route("GetUserPflanzen")]
        public string UserPflanzen([FromBody] UserSessionData userSessionData) => _manager.UserPflanzen(userSessionData);
        [HttpPost()]
        [Route("GetUserGruppen")]
        public string UserGruppen([FromBody] UserSessionData userSessionData) => _manager.UserGruppen(userSessionData);
        [HttpPost()]
        [Route("GetPflanzenArten")]
        public string PflanzenArten([FromBody] UserSessionData userSessionData) => _manager.PflanzenArten(userSessionData);

        [HttpPost()]
        [Route("AddPflanze")]
        public string PflanzeHinzufügen([FromBody] ActionMessage action) => _manager.PflanzeHinzufügen(action);
        [HttpPost()]
        [Route("AddGruppe")]
        public string GruppeHinzufügen([FromBody] ActionMessage action) => _manager.GruppeHinzufügen(action);



        [HttpPost()]
        [Route("Initialize")]
        public string Initialize([FromBody] UserSessionData userSessionData) => _manager.Initialize(userSessionData);
   
        [HttpPost()]
        [Route("Login")]
        public double Login([FromBody] LoginData json) => _manager.Login(json);
      
    }
}
