using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.StaffMemberModles
{
    internal class StaffMembersApiResponse
    {
        public string ApiVersion { get; set; }
        public  StaffMembersData ReferenceData { get; set; }
        public ApiError Error { get; set; }
    }
}
