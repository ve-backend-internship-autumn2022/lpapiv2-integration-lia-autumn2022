using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseGradeListApiResponse
    {
        public string? NextLink { get; set; }
        public CourseGrade[]? Data { get; set; }
    }
}
