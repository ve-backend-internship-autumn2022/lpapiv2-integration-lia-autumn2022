using LpApiIntegration.FetchFromV2.CourseModels;
using LpApiIntegration.FetchFromV2.StaffMemberModels;
using LpApiIntegration.FetchFromV2.StudentModels;

namespace LpApiIntegration.FetchFromV2.GroupModel
{
    internal class GroupsReferenceData
    {
        public StaffMember[] StaffMembers { get; set; }
        public FullStudent[] Students { get; set; }
        public CourseDefinition[] CourseDefinitions { get; set; }
        public GroupRole[] Grouproles { get; set; }

    }
}
