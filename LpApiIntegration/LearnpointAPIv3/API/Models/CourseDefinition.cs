using System.ComponentModel.DataAnnotations;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseDefinition
    {
        [Key]
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsInternship { get; set; }
        public int Points { get; set; }

    }
}
