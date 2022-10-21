using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.StudentModels
{
    internal class StudentGroup
    {
        public GroupReference Group { get; set; }
        public GroupRoleReference[] GroupRoles { get; set; }
    }
}
