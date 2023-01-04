using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LpApiIntegration.Db.Db.Models
{
    public class GradingModel
    {
        [Key] public int Id { get; set; }
        public int ExternalId { get; set; }
        public int GradedStudentId { get; set; }
        public int GradingStaffId { get; set; }
        public int? GradedCourseDefinitionId { get; set; }
        public int? GradedProgramEnrollmentId { get; set; }
        public string? Grade { get; set; }
        public string? GradeCode { get; set; }
        public double? GradePoints { get; set; }
        public string? OfficialGradingDate { get; set; }
        public DateTime Published { get; set; }
        public int? GradedCourseInstanceId { get; set; }
        public int BestCourseSelectionMeritSort { get; set; }

        [ForeignKey("GradedStudentId")] public StudentModel Student { get; set; }
        [ForeignKey("GradingStaffId")] public StaffModel StaffMember { get; set; }
        [ForeignKey("GradedCourseDefinitionId")] public CourseDefinitionModel CourseDefinition { get; set; }
        [ForeignKey("GradedCourseInstanceId")] public CourseModel Course { get; set; }
    }
}
