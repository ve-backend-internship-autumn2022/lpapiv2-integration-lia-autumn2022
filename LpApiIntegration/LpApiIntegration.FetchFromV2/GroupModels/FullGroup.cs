using LpApiIntegration.FetchFromV2.CourseModels;
using LpApiIntegration.FetchFromV2.GroupModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.GroupModel
{
    internal class FullGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? LifespanFrom { get; set; }
        public DateTime? LifespanUntil { get; set; }
        public GroupCategory Category { get; set; }
        public ParentGroupReference ParentGroup { get; set; }
        public ExtendedProperty[] ExtendedProperties { get; set; }
        public CourseDefinitionReference CourseDefinition { get; set; }
        public GroupStaffMember[] StaffGroupMembers { get; set; }
        public GroupStudent[] StudentGroupMembers { get; set; }


    }
}
