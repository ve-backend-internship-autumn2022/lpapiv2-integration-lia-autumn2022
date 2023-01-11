using LpApiIntegration.FetchFromV2.GroupModel;

namespace LpApiIntegration.FetchFromV2.StudentModels
{
    internal class StudentGroup
    {
        public GroupReference Group { get; set; }
        public GroupRoleReference[] GroupRoles { get; set; }
    }
}
