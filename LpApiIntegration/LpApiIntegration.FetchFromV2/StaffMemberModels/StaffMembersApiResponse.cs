namespace LpApiIntegration.FetchFromV2.StaffMemberModels
{
    internal class StaffMembersApiResponse
    {
        public string ApiVersion { get; set; }
        public StaffMembersData Data { get; set; }
        public ApiError Error { get; set; }
    }
}
