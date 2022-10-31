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
        public int Id { get; set; }
        public string? NationalRegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Email2 { get; set; }
        public string? MobilePhone { get; set; }
        public StudentHomeAddress? HomeAddress { get; set; }
        public string? HomePhone { get; set; }
        public StudentGroup[] Groups { get; set; }
        public StudentEducationPlan[] EducationPlan { get; set; }
    }
}
