namespace LpApiIntegration.FetchFromV2.CourseModels
{
    internal class CourseDefinition
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsInternship { get; set; }
        public int Points { get; set; }
    }
}
