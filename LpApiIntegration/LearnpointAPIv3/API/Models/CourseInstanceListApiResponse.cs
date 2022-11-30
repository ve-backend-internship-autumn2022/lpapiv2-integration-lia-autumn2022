using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseInstanceListApiResponse
    {
        public string? NextLink { get; set; }
        public CourseInstance[]? Data { get; set; }
    }
}
