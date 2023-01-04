using LpApiIntegration.FetchFromV2.StudentModels;

namespace LpApiIntegration.FetchFromV2.GroupModel
{
    internal class GroupStudent
    {
        public StudentReference Student { get; set; }

        public GroupRoleReference[] GroupRoles { get; set; }
    }
}
