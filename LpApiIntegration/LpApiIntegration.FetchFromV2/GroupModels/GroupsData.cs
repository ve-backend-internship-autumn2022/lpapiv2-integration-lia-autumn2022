namespace LpApiIntegration.FetchFromV2.GroupModel
{
    internal class GroupsData
    {
        public FullGroup[] Groups { get; set; }
        public FullGroup[] ParentGroups { get; set; }
        public GroupsReferenceData ReferenceData { get; set; }

    }
}
