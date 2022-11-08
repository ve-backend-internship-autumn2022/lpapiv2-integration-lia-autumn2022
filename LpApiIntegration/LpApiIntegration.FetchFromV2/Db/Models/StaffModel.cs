using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.Db.Models
{
    internal class StaffModel
    {
        [Key] public int Id { get; set; }
        public int ExternalId { get; set; }
        public string? NationalRegistrationNumber { get; set; }
        public string? Signature { get; set; }
        public string? FullName { get; set; }      
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Email2 { get; set; }
        public string? MobilePhone { get; set; }
        public bool MayExposeMobilePhoneToStudent { get; set; }
        public string? Phone2 { get; set; }
        public bool MayExposeMobilePhone2ToStudent { get; set; }
        //public ICollection<StaffCourseRelationModel> CourseMemberships { get; set; }

    }
}
