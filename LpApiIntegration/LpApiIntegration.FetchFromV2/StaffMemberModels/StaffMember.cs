namespace LpApiIntegration.FetchFromV2.StaffMemberModels
{
    internal class StaffMember
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? NationalRegistrationNumber { get; set; }
        public string? Signature { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Email2 { get; set; }
        public string? MobilePhone { get; set; }
        public bool MayExposeMobilePhoneToStudent { get; set; }
        public string? Phone2 { get; set; }
        public bool MayExposeMobilePhone2ToStudent { get; set; }
    }
}
