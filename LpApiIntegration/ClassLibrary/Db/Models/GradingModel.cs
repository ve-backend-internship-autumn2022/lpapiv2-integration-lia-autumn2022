using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.Db.Db.Models
{
    public class GradingModel
    {
        [Key] public int Id { get; set; }
        public int GradedStudentId { get; set; }
        public int GradingStaffId { get; set; }
        public int GradedCourseId { get; set; }
        public int? GradedProgramEnrollmentId { get; set; }
        public string? Grade { get; set; }
        public string? GradeCode { get; set; }
        public double? GradePoints { get; set; }
        public string? OfficialGradingDate { get; set; }
        public DateTime Published { get; set; }
        public int? GradedCourseInstanceId { get; set; }
        [ForeignKey("GradedStudentId")] public StudentModel Student { get; set; }
        [ForeignKey("GradingStaffId")] public StaffModel StaffMember { get; set; }
        [ForeignKey("GradedProgramEnrollmentId")] public ProgramModel Program { get; set; }
        [ForeignKey("GradedCourseId")] public CourseModel Course { get; set; }

    }
}
