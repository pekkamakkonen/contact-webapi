using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ContactsWebApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ContactsWebApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AzureSettings _azureSettings;

        public AuthenticationService(IOptions<AzureSettings> settings)
        {
            _azureSettings = settings.Value;
        }

        public async Task<AccessToken> AuthenticateUser(UserCredentials userCredentials)
        {
            var authenticationParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", _azureSettings.ApplicationId),
                new KeyValuePair<string, string>("resource", _azureSettings.Resource),
                new KeyValuePair<string, string>("username", userCredentials.Username),
                new KeyValuePair<string, string>("password", userCredentials.Password),
                new KeyValuePair<string, string>("grant_type", _azureSettings.GrantType),
                new KeyValuePair<string, string>("client_secret", _azureSettings.Key)
            };

            AccessToken token = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                HttpContent content = new FormUrlEncodedContent(authenticationParams);
                var response = await httpClient.PostAsync(_azureSettings.AuthenticationEndpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    token = JsonConvert.DeserializeObject<AccessToken>(data);
                }
            }
            return token;
        }
    }
}
