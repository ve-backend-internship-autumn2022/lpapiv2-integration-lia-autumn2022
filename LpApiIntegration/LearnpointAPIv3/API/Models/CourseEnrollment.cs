using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseEnrollment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseInstanceId { get; set; }
    }
}
