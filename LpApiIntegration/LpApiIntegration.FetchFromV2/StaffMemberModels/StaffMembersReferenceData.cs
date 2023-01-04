using LpApiIntegration.FetchFromV2.GroupModel;

namespace LpApiIntegration.FetchFromV2.StaffMemberModels
{
    internal class StaffMembersReferenceData
    {
        public Group[] Groups { get; set; }
        public GroupRole[] GroupRoles { get; set; }
    }
}
