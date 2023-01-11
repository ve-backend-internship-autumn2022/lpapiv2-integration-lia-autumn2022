using LpApiIntegration.Db.Db.Models;
using System.ComponentModel.DataAnnotations;

namespace LpApiIntegration.Db
{
    public class StaffModel
    {
        [Key] public int Id { get; set; }
        public int ExternalId { get; set; }
        public int ExternalUserId { get; set; }
        public string? NationalRegistrationNumber { get; set; }
        public string? Signature { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Email2 { get; set; }
        public string? MobilePhone { get; set; }
        public bool MayExposeMobilePhoneToStudents { get; set; }
        public string? Phone2 { get; set; }
        public bool MayExposePhone2ToStudents { get; set; }

        public ICollection<StaffCourseRelationModel> CourseMemberships { get; set; }
        public ICollection<GradingModel> StudentGrade { get; set; }

    }
}
