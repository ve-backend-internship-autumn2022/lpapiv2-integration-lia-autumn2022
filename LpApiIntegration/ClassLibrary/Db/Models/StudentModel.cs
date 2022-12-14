
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.Db
{
    public class StudentModel
    {
        [Key] public int Id { get; set; }
        public int ExternalId { get; set; }
        public int ExternalUserId { get; set; }
        public string? NationalRegistrationNumber { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Email2 { get; set; }
        public string? MobilePhone { get; set; }
        public string? HomePhone { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; }
        public ICollection<StudentCourseRelationModel> CourseMemberships { get; set; }

    }
}
