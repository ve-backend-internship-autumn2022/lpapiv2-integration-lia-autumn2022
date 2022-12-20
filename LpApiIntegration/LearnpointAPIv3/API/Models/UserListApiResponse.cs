namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class UserListApiResponse
    {
        public string NextLink { get; set; }
        public User[] Data { get; set; }
    }
}
