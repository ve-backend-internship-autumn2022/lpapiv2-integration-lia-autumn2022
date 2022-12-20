namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class ProgramEnrollmentListApiResponse
    {
        public string? NextLink { get; set; }
        public ProgramEnrollment[]? Data { get; set; }
    }
}
