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
        [Key] public int Id { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public StudentModel Student { get; set; }

        public CourseModel Course { get; set; }
    }
}
