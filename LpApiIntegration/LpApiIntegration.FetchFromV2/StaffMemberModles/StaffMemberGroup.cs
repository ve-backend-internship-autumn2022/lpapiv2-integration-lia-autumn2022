using LpApiIntegration.FetchFromV2.GroupModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.StaffMemberModles
{
    internal class StaffMemberGroup
    {
        public GroupReference Group { get; set; }
        public bool IsGroupManager { get; set; }
        public GroupRoleReference[] GroupRoles { get; set; }
    }
}
