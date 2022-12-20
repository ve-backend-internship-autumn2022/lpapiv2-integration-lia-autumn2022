namespace LpApiIntegration.FetchFromV3.API.Models
{
    internal class ProgramInstanceListApiResponse
    {
        public string NextLink { get; set; }
        public ProgramInstance[] Data { get; set; }
    }
}
