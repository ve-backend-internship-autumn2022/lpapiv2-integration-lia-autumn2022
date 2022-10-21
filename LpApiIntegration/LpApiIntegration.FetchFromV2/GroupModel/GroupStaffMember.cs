using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.GroupModel
{
    public class GroupStaffMember
    {
        //public StaffMemberReference StaffMember { get; set; }
        public bool IsGroupManager { get; set; }
        public GroupRoleReference[] GroupRoles { get; set; }
    }
}
