using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [Route("PostPflanzen")]
        public string UserPflanzen([FromBody] string user, double sessionid) => _manager.UserPflanzen(user, sessionid);
        [HttpPost()]
        [Route("PostGruppen")]
        public string UserGruppen([FromBody] string user, double sessionid) => _manager.UserGruppen(user, sessionid);
        [HttpPost()]
        [Route("PostPflanzenArten")]
        public string PflanzenArten([FromBody] string user, double sessionid) => _manager.PflanzenArten(user, sessionid);

        [HttpPost()]
        [Route("Initialize")]
        public string Initialize([FromBody] string user, double sessionid) => _manager.Initialize(user, sessionid);
        [HttpPost()]
        [Route("Login")]
        public double Login([FromBody] string user, string password) => _manager.Login(user, password);
    }
}
