using LpApiIntegration.FetchFromV2.StudentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.GroupModel
{
    internal class GroupsReferenceData
    {
        //public StaffMember[] StaffMembers { get; set; }
        public Student[] Students { get; set; }
        //public CourseDefinition[] CourseDefinitions { get; set; }
        public GroupRole[] Grouproles { get; set; }

    }
}
