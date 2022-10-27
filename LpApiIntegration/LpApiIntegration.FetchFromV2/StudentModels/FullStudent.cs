using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.StudentModels
{
    internal class FullStudent
    {
        [Key]public int DbId { get; set; }
        public int Id { get; set; }
        public string? NationalRegistrationNumber { get; set; }
        [NotMapped] public string FirstName { get; set; }
        [NotMapped] public string LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Email2 { get; set; }
        public string? MobilePhone { get; set; }
        [NotMapped] public StudentHomeAddress? HomeAddress { get; set; }
        public string? HomePhone { get; set; }
        [NotMapped] public StudentGroup[] Groups { get; set; }
        [NotMapped] public StudentEducationPlan[] EducationPlan { get; set; }
        public string? FullName { get; set; }
    }
}
