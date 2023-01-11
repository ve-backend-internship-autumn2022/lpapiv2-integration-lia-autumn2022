namespace LpApiIntegration.FetchFromV2.StudentModels
{
    internal class StudentsApiResponse
    {
        public string ApiVersion { get; set; }
        public StudentsData Data { get; set; }
        public ApiError Error { get; set; }
    }
}
