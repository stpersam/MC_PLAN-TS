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
        public bool RegisterUser([FromBody] string user, string password, string email) => _manager.RegisterUser(user, password, email);

        [HttpPost()]
        [Route("GetUserPflanzen")]
        public string UserPflanzen([FromBody] string user, double sessionid) => _manager.UserPflanzen(user, sessionid);
        [HttpPost()]
        [Route("GetUserGruppen")]
        public string UserGruppen([FromBody] string user, double sessionid) => _manager.UserGruppen(user, sessionid);
        [HttpPost()]
        [Route("GetPflanzenArten")]
        public string PflanzenArten([FromBody] string user, double sessionid) => _manager.PflanzenArten(user, sessionid);

        [HttpPost()]
        [Route("AddPflanze")]
        public string PflanzeHinzufügen([FromBody] string pflanze, string user, double sessionid) => _manager.PflanzeHinzufügen(pflanze, user, sessionid);
        [HttpPost()]
        [Route("AddGruppe")]
        public string GruppeHinzufügen([FromBody] string gruppe, string user, double sessionid) => _manager.GruppeHinzufügen(gruppe, user, sessionid);



        [HttpPost()]
        [Route("Initialize")]
        public string Initialize([FromBody] string user, double sessionid) => _manager.Initialize(user, sessionid);
   
        [HttpPost()]
        [Route("Login")]
        public double Login([FromBody] LoginData json) => _manager.Login(json);
      
    }
}
