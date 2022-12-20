namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class Group
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public GroupCategory Category { get; set; }
        public int? ParentGroupId { get; set; }
        public int? CourseInstanceId { get; set; }
        public int? ProgramInstanceId { get; set; }
    }
}
