using ContactsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsWebApi.Services
{
    public interface IAuthenticationService
    {
        Task<AccessToken> AuthenticateUser(UserCredentials userCredentials);
        Task AuthenticateUser(string username, string password);
    }
}
