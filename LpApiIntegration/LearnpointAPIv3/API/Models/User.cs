using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class User
    {
        public int Id { get; set; }
        public bool IsStudent { get; set; }
        public bool IsActiveStudent { get; set; }
        public bool IsActiveStaff { get; set; }
        public string? NationalRegistrationNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Email2 { get; set; }
        public string? Phone { get; set; }
        public string? Phone2 { get; set; }
        public string[]? StaffFunctions { get; set; }
    }
}
