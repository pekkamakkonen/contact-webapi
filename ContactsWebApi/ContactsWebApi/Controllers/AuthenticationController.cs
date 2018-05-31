using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactsWebApi.Models;
using ContactsWebApi.Services;

namespace ContactsWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;        

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserCredentials userCredentials)
        {
            var token = await _authenticationService.AuthenticateUser(userCredentials);
            if (token != null)
            {
                return new JsonResult(token);
            }

            return new UnauthorizedResult();
        }
    }
}