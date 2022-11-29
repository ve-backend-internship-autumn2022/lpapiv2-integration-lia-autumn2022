using LearnpointAPIv3;
using LearnpointAPIv3.API;
using LpApiIntegration.FetchFromV3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

using IHost host = Host.CreateDefaultBuilder(args).Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

ApiSettings apiSettings = config.GetRequiredSection("ApiSettings").Get<ApiSettings>();

// Application code should start here.

// Fetching data from API
string jsonCoursedefinitions = FetchFromApi.GetCoursedefinitions(apiSettings);
//string jsonStudents = FetchFromApi.GetStudents(apiSettings);
//string jsonStudentsExtended = FetchFromApi.GetStudentsExtended(apiSettings);
//string jsonGroups = FetchFromApi.GetGroups(apiSettings);
//string jsonGroupsExtended = FetchFromApi.GetGroupsExtended(apiSettings);
//string jsonStaffMembers = FetchFromApi.GetStaffMembers(apiSettings);

//Deserializing json to response object
var coursedefinitionResponse = JsonSerializer.Deserialize<CourseDefinitionListApiResponse>(jsonCoursedefinitions);
//var studentResponse = JsonSerializer.Deserialize<StudentsApiResponse>(jsonStudents);
//var studentResponseExtended = JsonSerializer.Deserialize<StudentsApiResponse>(jsonStudentsExtended);

Console.WriteLine(coursedefinitionResponse.Data[2].Code);

//var groupResponse = JsonSerializer.Deserialize<GroupsApiResponse>(jsonGroups);
//var groupResponseExtended = JsonSerializer.Deserialize<GroupsApiResponse>(jsonGroupsExtended);

//var staffResponse = JsonSerializer.Deserialize<StaffMembersApiResponse>(jsonStaffMembers);

//Saving json to file
File.WriteAllText("Coursedefinitions.json", jsonCoursedefinitions);
//File.WriteAllText("StudentsExtended.json", jsonStudentsExtended);

//File.WriteAllText("Groups.json", jsonGroups);
//File.WriteAllText("GroupsExtended.json", jsonGroupsExtended);

//File.WriteAllText("StaffMembers.json", jsonStaffMembers);

// Database manager
//DbManager.StudentManager(studentResponse);
//DbManager.CourseManager(groupResponse);
//DbManager.StaffManager(staffResponse);
//DbManager.ProgramManager(groupResponseExtended);
//DbManager.RelationshipManager(groupResponseExtended, groupResponse, studentResponse);

await host.RunAsync();