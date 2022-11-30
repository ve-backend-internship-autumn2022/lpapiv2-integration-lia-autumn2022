using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class SelectCourseDefinition
    {
        public int CourseDefinitionId { get; set; }
        public int? SpecializationId { get; set; }
    }
}
