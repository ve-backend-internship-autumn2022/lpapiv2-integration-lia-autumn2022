using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LpApiIntegration.FetchFromV2;
using LpApiIntegration.FetchFromV2.StudentModels;
using LpApiIntegration.FetchFromV2.GroupModel;
using System.Text;
using System.Text.Json;
using LpApiIntegration.FetchFromV2.StaffMemberModles;

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

// Students
string jsonStudents = client.GetStringAsync($"/bulkapi/v2/{apiSettings.TenantIdentifier}/students").Result;

// Testing Deserializing json to object
var studentResponse = JsonSerializer.Deserialize<StudentsApiResponse>(jsonStudents);

//foreach (var item in studentResponse.Data.ReferenceData.Groups)
//{
//    if (item.ExtendedProperties != null)
//    {
//        Console.WriteLine($"\n{item.Name} {item.Code}");
//        for (int groupNumber = 0; groupNumber < item.ExtendedProperties.Length; groupNumber++)
//        {
//            Console.WriteLine(item.ExtendedProperties[groupNumber].Value);
//        }
//    }
//}

// Groups
string jsonGroups = client.GetStringAsync($"/bulkapi/v2/{apiSettings.TenantIdentifier}/groups").Result;

// Testing Deserializing json to object
//var groupResponse = JsonSerializer.Deserialize<GroupsApiResponse>(jsonGroups);
//Console.WriteLine(groupResponse.ApiVersion);

//foreach (var item in groupResponse.Data.ParentGroups)
//{
//    Console.WriteLine($"\n{item.Name} {item.Code}\n");
//    for (int groupNumber = 0; groupNumber < item.Groups.Length; groupNumber++)
//    {
//        Console.WriteLine(item.Groups[groupNumber].IsGroupManager);
//    }
//}

// Staff
string jsonStaffMembers = client.GetStringAsync($"/bulkapi/v2/{apiSettings.TenantIdentifier}/staffmembers").Result;

// Testing Deserializing json to object
var staffResponse = JsonSerializer.Deserialize<StaffMembersApiResponse>(jsonStaffMembers);

//foreach (var item in staffResponse.Data.StaffMembers)
//{
//    Console.WriteLine($"\n{item.FirstName} {item.LastName}\n");
//    for (int groupNumber = 0; groupNumber < item.Groups.Length; groupNumber++)
//    {
//        Console.WriteLine(item.Groups[groupNumber].IsGroupManager);
//    }
//}

await host.RunAsync();