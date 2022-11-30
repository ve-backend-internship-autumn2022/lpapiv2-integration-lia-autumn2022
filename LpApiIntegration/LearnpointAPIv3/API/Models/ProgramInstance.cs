using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class ProgramInstance
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? Points { get; set; }
        public Specialization[]? Specializations { get; set; }
        public ExtendedProperty[]? ExtendedProperties { get; set; }
    }
}
