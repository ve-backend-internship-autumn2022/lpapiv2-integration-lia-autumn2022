using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.StudentModels
{
    internal class StudentEducationPlanState
    {
        public string Name { get; set; }
        public bool IsActiveStudent { get; set; }
        public DateTime? FromDate { get; set; }
    }
}
