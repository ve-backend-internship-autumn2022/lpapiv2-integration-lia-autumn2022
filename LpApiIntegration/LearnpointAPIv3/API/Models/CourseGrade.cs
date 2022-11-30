using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseGrade
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseDefinitionId { get; set; }
        public int? ProgramEnrollmentId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public double? Value { get; set; }
        public GradeScale Scale { get; set; }
        public string? OfficialGradingDate { get; set; }
        public DateTime Published { get; set; }
        public int? AwardedInCourseInstanceId { get; set; }
        public int AwardedByUserId { get; set; }
        public int BestCourseSelectionMeritSort { get; set; }

    }
}
