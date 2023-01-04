namespace LpApiIntegration.FetchFromV2.GroupModel
{
    internal class GroupsApiResponse
    {
        public string ApiVersion { get; set; }
        public GroupsData Data { get; set; }
        public ApiError Error { get; set; }
    }
}
