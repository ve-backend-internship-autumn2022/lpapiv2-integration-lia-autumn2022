using LpApiIntegration.FetchFromV2.GroupModel;

namespace LpApiIntegration.FetchFromV2.StaffMemberModels
{
    internal class StaffMemberGroup
    {
        public GroupReference Group { get; set; }
        public bool IsGroupManager { get; set; }
        public GroupRoleReference[] GroupRoles { get; set; }
    }
}
