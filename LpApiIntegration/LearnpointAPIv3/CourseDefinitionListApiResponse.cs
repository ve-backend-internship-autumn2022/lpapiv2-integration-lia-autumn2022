using LpApiIntegration.FetchFromV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3
{
    internal class CourseDefinitionListApiResponse
    {
        public string? NextLink { get; set; }
        public CourseDefinition[] Data { get; set; }
    }
}
