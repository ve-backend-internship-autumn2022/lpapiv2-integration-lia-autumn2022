using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.Db.Db.Models
{
    public class ProgramEnrollmentModel
    {
        [Key] public int Id { get; set; }
        public int ExternalId { get; set; }
        public int StudentId { get; set; }
        public int ProgramInstanceId { get; set; }
        public DateTime Enrolled { get; set; }
        public DateTime? Unenrolled { get; set; }
        public bool Active { get; set; }
        public bool Canceled { get; set; }
        public DateTime Changed { get; set; }
        public string? DiplomaDate { get; set; }
        

    }
}
