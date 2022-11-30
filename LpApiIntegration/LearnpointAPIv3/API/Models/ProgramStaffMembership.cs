using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class ProgramStaffMembership
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProgramInstanceId { get; set; }
        public bool IsManager { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
