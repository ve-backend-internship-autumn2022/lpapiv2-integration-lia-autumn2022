using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.StaffMemberModels
{
    internal class FullStaffMember
    {
        public int Id { get; set; }
        public string? NationalRegistrationNumber { get; set; }
        public string? Signature { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Email2 { get; set; }
        public string? MobilePhone { get; set; }
        public bool MayExposeMobilePhoneToStudents { get; set; }
        public string? Phone2 { get; set; }
        public bool MayExposePhone2ToStudents { get; set; }
        public string[] StaffFunctions { get; set; }
        public StaffMemberGroup[] Groups { get; set; }
    }
}
