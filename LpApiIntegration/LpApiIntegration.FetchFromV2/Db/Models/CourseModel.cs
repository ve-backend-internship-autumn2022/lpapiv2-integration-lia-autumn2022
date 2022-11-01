using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.Db.Models
{
    internal class CourseModel
    {
        [Key] public int DbId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; } 
        public DateTime? LifespanFrom { get; set; }
        public DateTime? LifespanUntil { get; set; }        
        public int? Points { get; set; }
    }
}
