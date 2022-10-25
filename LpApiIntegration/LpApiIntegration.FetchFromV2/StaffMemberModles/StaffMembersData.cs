using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.StaffMemberModles
{
    internal class StaffMembersData
    {
        public FullStaffMember[] StaffMembers { get; set; }
        public StaffMembersReferenceData ReferenceData { get; set; }
    }
}
