using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnpointAPIv3
{
    internal class ApiSettings
    {
        public string? ApiBaseAddress { get; set; }
        public string? TokenEndpointUri { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? RequestedScopes { get; set; }
        public string? TenantIdentifier { get; set; }
    }
}
