using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseInstance
    {
        public int Id { get; set; }
        public int CourseDefinitionId { get; set; }
        public string? Code { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
