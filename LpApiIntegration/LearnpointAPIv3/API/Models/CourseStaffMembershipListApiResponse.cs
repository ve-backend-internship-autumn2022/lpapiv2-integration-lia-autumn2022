namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseStaffMembershipListApiResponse
    {
        public string? NextLink { get; set; }
        public CourseStaffMembership[] Data { get; set; }
    }
}
