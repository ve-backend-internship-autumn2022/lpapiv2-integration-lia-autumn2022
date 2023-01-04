namespace LpApiIntegration.FetchFromV2.GroupModel
{
    internal class ParentGroupReference
    {
        public GroupReference Group { get; set; }
        public ParentGroupReference ParentGroup { get; set; }
    }
}
