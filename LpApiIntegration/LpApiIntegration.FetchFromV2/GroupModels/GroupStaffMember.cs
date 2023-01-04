using LpApiIntegration.FetchFromV2.StaffMemberModels;

namespace LpApiIntegration.FetchFromV2.GroupModel
{
    internal class GroupStaffMember
    {
        public StaffMemberReference StaffMember { get; set; }
        public bool IsGroupManager { get; set; }
        public GroupRoleReference[] GroupRoles { get; set; }
    }
}
