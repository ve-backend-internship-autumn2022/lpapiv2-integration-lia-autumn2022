using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.Db.Db.Models
{
    public class CourseDefinitionModel
    {
        [Key]
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsInternship { get; set; }
        public int Points { get; set; }

        public ICollection<GradingModel> Gradings { get; set; }
    }
}
