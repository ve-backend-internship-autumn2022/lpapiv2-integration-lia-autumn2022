using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class GroupMembershipListApiResponse
    {
        public string? NextLink { get; set; }
        public GroupMembership[]? Data { get; set; }
    }
}
