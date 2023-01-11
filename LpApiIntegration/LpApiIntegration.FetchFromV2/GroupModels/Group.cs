namespace LpApiIntegration.FetchFromV2.GroupModel
{
    internal class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? LifespanFrom { get; set; }
        public DateTime? LifespanUntil { get; set; }
        public GroupCategory Category { get; set; }
        public ParentGroupReference ParentGroup { get; set; }
        public ExtendedProperty[] ExtendedProperties { get; set; }

    }
}
