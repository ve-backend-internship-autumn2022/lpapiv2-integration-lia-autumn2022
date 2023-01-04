using LpApiIntegration.Db.Db.Models;
using System.ComponentModel.DataAnnotations;

namespace LpApiIntegration.Db
{
    public class ProgramModel
    {
        [Key] public int Id { get; set; }
        public int ExternalId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime? LifespanFrom { get; set; }
        public DateTime? LifespanUntil { get; set; }

        public ICollection<ProgramEnrollmentModel> ProgramEnrollments { get; set; }
    }
}
