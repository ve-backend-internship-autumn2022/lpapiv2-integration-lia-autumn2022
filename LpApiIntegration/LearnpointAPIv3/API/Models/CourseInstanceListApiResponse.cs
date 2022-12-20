namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseInstanceListApiResponse
    {
        public string? NextLink { get; set; }
        public CourseInstance[]? Data { get; set; }
    }
}
