using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.GroupModel
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? LifespanFrom { get; set; }
        public DateTime? LifespanUntil { get; set; }
        public GroupCategory Category { get; set; }
        //ParentGroup(ParentGroupReference optional) Omitted if no parent group,
        //ExtendedProperties(ExtendedProperty) Omitted if no Extended properties


    }
}
