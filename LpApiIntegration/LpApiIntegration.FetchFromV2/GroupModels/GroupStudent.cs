using LpApiIntegration.FetchFromV2.StudentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.GroupModel
{
    internal class GroupStudent
    {
        public StudentReference Student { get; set; }

        public GroupRoleReference GroupRoles { get; set; }

    }
}
