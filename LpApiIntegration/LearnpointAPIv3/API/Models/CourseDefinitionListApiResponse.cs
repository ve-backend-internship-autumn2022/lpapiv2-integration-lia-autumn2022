using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseDefinitionListApiResponse
    {
        public string? NextLink { get; set; }
        public CourseDefinition[] Data { get; set; }
    }
}
