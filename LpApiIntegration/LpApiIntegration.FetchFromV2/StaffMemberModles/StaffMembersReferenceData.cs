using LpApiIntegration.FetchFromV2.GroupModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.StaffMemberModles
{
    internal class StaffMembersReferenceData
    {
        public Group[] Groups { get; set; }
        public GroupRole[] GroupRoles { get; set; }
    }
}
