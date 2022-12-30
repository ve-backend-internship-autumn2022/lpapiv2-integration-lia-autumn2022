using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.Db.Db.Models
{
    public class CourseGradeModel
    {
        [Key] public int Id { get; set; }
        public int ExternalId { get; set; }
        public int ExternalStudentId { get; set; }
        public int ExternalGradingStaffId { get; set; }
        public int ExternalCourseDefinitionId { get; set; }
        public int? ExternalProgramEnrollmentId { get; set; }
        public string? Grade { get; set; }
        public string? GradeCode { get; set; }
        public double? GradePoints { get; set; }
        public string? OfficialGradingDate { get; set; }
        public DateTime Published { get; set; }
        public int? GradedCourseInstanceId { get; set; }

    }
}
