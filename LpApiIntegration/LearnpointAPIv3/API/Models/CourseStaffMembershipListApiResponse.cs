using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseStaffMembershipListApiResponse
    {
        public string? NextLink { get; set; }
        public CourseStaffMembership[] Data { get; set; }
    }
}
