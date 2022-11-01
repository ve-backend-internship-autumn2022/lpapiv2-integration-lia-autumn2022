using LpApiIntegration.FetchFromV2.StudentModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.Db.Models
{
    internal class StudentCourseRelationModel
    {
        [Key] public int DbId { get; set; }

        [ForeignKey("DbId")] public int StudentId { get; set; }

        public int CourseId { get; set; }
    }
}
