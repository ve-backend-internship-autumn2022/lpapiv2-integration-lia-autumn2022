namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class GroupMembership
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public bool IsManager { get; set; }
        public bool IsAdministrator { get; set; }
        public GroupRole[]? Roles { get; set; }
    }
}
