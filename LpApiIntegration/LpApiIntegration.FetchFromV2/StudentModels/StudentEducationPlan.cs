using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.StudentModels
{
    internal class StudentEducationPlan
    {
        public int Id { get; set; }
        public EducationPlan[] Parts { get; set; }
        public StudentEducationPlanState State { get; set; }
    }
}
