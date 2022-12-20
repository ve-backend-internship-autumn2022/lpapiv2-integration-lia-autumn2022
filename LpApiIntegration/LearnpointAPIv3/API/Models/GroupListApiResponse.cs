namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class GroupListApiResponse
    {
        public string? NextLink { get; set; }
        public Group[] Data { get; set; }

    }
}
