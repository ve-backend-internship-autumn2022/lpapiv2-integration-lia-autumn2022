using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;      


namespace LpApiIntegration.Db
{
    public class StudentProgramRelationModel
    {
        [Key] public int Id { get; set; }
        public int StudentId { get; set; }
        public int ProgramId { get; set; }
        public bool IsActiveStudent { get; set; }
        public string? StateName { get; set; }
        public DateTime? FromDate { get; set; }
        public StudentModel Student { get; set; }
        public ProgramModel Program { get; set; }
        
    }
}
