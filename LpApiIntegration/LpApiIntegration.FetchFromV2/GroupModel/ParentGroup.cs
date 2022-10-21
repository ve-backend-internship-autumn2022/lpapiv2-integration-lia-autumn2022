using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LpApiIntegration.FetchFromV2.GroupModel.ParentGroup;

namespace LpApiIntegration.FetchFromV2.GroupModel
{
    public class ParentGroup
    {
         public int Id { get; set; }
         public string Name { get; set; }
         public string Code { get; set; }
         public DateTime LifespanFrom { get; set; }
         public DateTime LifespanUntil { get; set; }
         public GroupCategory Category { get; set; }
         public ParentGroup ParentGroups { get; set; }
         public ExtendedProperty[] ExtendedProperties { get; set; }
        //public Staffgroupmember1[] StaffGroupMembers { get; set; }
        //public Studentgroupmember1[] StudentGroupMembers { get; set; }

    }
}
