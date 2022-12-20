namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseGradeListApiResponse
    {
        public string? NextLink { get; set; }
        public CourseGrade[]? Data { get; set; }
    }
}
