using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.GroupModel
{
    public class ParentGroupReference
    {
        public GroupReference Group { get; set; }
        public ParentGroupReference ParentGroup { get; set; }
    }
}
