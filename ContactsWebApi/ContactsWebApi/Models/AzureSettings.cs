using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsWebApi.Models
{
    public class AzureSettings
    {
        public string DirectoryId { get; set; }
        public string ApplicationId { get; set; }
        public string Resource { get; set; }
        public string GrantType { get; set; }
        public string Key { get; set; }
        public string AuthenticationEndpoint { get; set; }
    }
}
