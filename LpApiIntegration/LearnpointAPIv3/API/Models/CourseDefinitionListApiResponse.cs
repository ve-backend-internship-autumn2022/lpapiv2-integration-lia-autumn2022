namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class CourseDefinitionListApiResponse
    {
        public string? NextLink { get; set; }
        public CourseDefinition[] Data { get; set; }
    }
}
