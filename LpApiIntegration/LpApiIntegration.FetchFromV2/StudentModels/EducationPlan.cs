using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.StudentModels
{
    internal class EducationPlan
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public EducationPlanType Type { get; set; }
    }
}
