using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.Db.Models
{
    internal class StaffCourseRelationModel
    {
        [Key] public int Id { get; set; }

        public int StaffId { get; set; }
        public StaffModel StaffMember { get; set; }

        public int CourseId { get; set; }
        public CourseModel Course { get; set; }
    }
}
