using LpApiIntegration.FetchFromV2;
using LpApiIntegration.FetchFromV2.API;
using LpApiIntegration.FetchFromV2.Db;
using LpApiIntegration.FetchFromV2.GroupModel;
using LpApiIntegration.FetchFromV2.StaffMemberModels;
using LpApiIntegration.FetchFromV2.StudentModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

using IHost host = Host.CreateDefaultBuilder(args).Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

ApiSettings apiSettings = config.GetRequiredSection("ApiSettings").Get<ApiSettings>();

// Application code should start here.

// Fetching data from API
string jsonStudents = FetchFromApi.GetStudents(apiSettings);
string jsonStudentsExtended = FetchFromApi.GetStudentsExtended(apiSettings);
string jsonGroups = FetchFromApi.GetGroups(apiSettings);
string jsonGroupsExtended = FetchFromApi.GetGroupsExtended(apiSettings);
string jsonStaffMembers = FetchFromApi.GetStaffMembers(apiSettings);

//Deserializing json to response object
var studentResponse = JsonSerializer.Deserialize<StudentsApiResponse>(jsonStudents);
var studentResponseExtended = JsonSerializer.Deserialize<StudentsApiResponse>(jsonStudentsExtended);

var groupResponse = JsonSerializer.Deserialize<GroupsApiResponse>(jsonGroups);
var groupResponseExtended = JsonSerializer.Deserialize<GroupsApiResponse>(jsonGroupsExtended);

var staffResponse = JsonSerializer.Deserialize<StaffMembersApiResponse>(jsonStaffMembers);

//Saving json to file
//File.WriteAllText("Students.json", jsonStudents);
//File.WriteAllText("StudentsExtended.json", jsonStudentsExtended);

//File.WriteAllText("Groups.json", jsonGroups);
//File.WriteAllText("GroupsExtended.json", jsonGroupsExtended);

//File.WriteAllText("StaffMembers.json", jsonStaffMembers);

// Database manager
DbManager.StudentManager(studentResponse);
DbManager.CourseManager(groupResponse);
DbManager.StaffManager(staffResponse);
DbManager.ProgramManager(groupResponseExtended);
DbManager.RelationshipManager(groupResponseExtended, groupResponse, studentResponse);

await host.RunAsync();