namespace LpApiIntegration.FetchFromV2.StudentModels
{
    internal class FullStudent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? NationalRegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Email2 { get; set; }
        public string? MobilePhone { get; set; }
        public StudentHomeAddress? HomeAddress { get; set; }
        public string? HomePhone { get; set; }
        public StudentGroup[] Groups { get; set; }
        public StudentEducationPlan[] EducationPlans { get; set; }
    }
}
