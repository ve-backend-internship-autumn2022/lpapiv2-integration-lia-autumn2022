using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LpApiIntegration.FetchFromV2;
using System.Text;
using System.Text.Json;

using IHost host = Host.CreateDefaultBuilder(args).Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

ApiSettings apiSettings = config.GetRequiredSection("ApiSettings").Get<ApiSettings>();


// Application code should start here.

//Get Access token
var tokenHttpClient = new HttpClient();
var credentials = $"{apiSettings.ClientId}:{apiSettings.ClientSecret}";
var base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
tokenHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
var tokenResponse = tokenHttpClient.PostAsync(apiSettings.TokenEndpointUri,
                                                new FormUrlEncodedContent(new Dictionary<string, string>{
                                                                { "grant_type", "client_credentials" },
                                                                { "scope", apiSettings.RequestedScopes }
                                                    })).Result;
var tokenResponseContent = tokenResponse.Content.ReadAsStringAsync().Result;
//Construct a JSON document from the response and extract the AccessToken
var tokenResponseJSON = JsonDocument.Parse(tokenResponseContent);
string accessToken = null;
if (tokenResponseJSON.RootElement.TryGetProperty("access_token", out JsonElement accessTokenJSONValue))
{
    accessToken = accessTokenJSONValue.ToString();
}

//Fetch from API
var client = new HttpClient
{
    BaseAddress = new Uri(apiSettings.ApiBaseAddress)
};
client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

string jsonStudents = client.GetStringAsync($"/bulkapi/v2/{apiSettings.TenantIdentifier}/students").Result;
Console.WriteLine(jsonStudents);

string jsonGroups = client.GetStringAsync($"/bulkapi/v2/{apiSettings.TenantIdentifier}/groups").Result;
Console.WriteLine(jsonGroups);

string jsonStaffMembers = client.GetStringAsync($"/bulkapi/v2/{apiSettings.TenantIdentifier}/staffmembers").Result;
Console.WriteLine(jsonStaffMembers);

await host.RunAsync();