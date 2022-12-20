namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class Specialization
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? Points { get; set; }
        public int? ParentSpecializationId { get; set; }
    }
}
