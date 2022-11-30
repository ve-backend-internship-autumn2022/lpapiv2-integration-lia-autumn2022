using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class ProgramEnrollment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProgramInstanceId { get; set; }
        public DateTime Enrolled { get; set; }
        public DateTime? Unenrolled { get; set; }
        public enum State { Active, Canceled, Finished }
        public string? StateDescription { get; set; }
        public DateTime Changed { get; set; }
        public string? DiplomaDate { get; set; }
        public Specialization[]? SelectedSpecializations { get; set; }
        public SelectCourseDefinition[]? SelectCourseDefinitions { get; set; }
    }
}