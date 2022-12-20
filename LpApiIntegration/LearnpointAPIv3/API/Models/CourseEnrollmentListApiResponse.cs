namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseEnrollmentListApiResponse
    {
        public string? NextLink { get; set; }
        public CourseEnrollment[] Data { get; set; }
    }
}
