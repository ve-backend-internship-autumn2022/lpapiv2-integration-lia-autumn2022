namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class GroupMembershipListApiResponse
    {
        public string? NextLink { get; set; }
        public GroupMembership[]? Data { get; set; }
    }
}
