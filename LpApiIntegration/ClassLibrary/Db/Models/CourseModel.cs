using LpApiIntegration.Db.Db.Models;
using System.ComponentModel.DataAnnotations;

namespace LpApiIntegration.Db
{
    public class CourseModel
    {
        [Key] public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? LifespanFrom { get; set; }
        public DateTime? LifespanUntil { get; set; }
        public int? Points { get; set; }

        public ICollection<StudentCourseRelationModel> StudentMemberships { get; set; }
        public ICollection<StaffCourseRelationModel> StaffMemberships { get; set; }
        public ICollection<GradingModel> Gradings { get; set; }
    }
}
