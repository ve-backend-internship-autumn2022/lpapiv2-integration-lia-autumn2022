namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class ApiResponse<T>
    {
        public string? NextLink { get; set; }
        public T Data { get; set; }
    }
}
