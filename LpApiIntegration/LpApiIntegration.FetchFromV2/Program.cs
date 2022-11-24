using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LpApiIntegration.FetchFromV2;
using LpApiIntegration.FetchFromV2.StudentModels;
using LpApiIntegration.FetchFromV2.GroupModel;
using System.Text;
using System.Text.Json;
using LpApiIntegration.FetchFromV2.StaffMemberModels;
using LpApiIntegration.FetchFromV2.API;
using Microsoft.EntityFrameworkCore;
using LpApiIntegration.FetchFromV2.Db;
using LpApiIntegration.FetchFromV2.Db.Models;

using IHost host = Host.CreateDefaultBuilder(args).Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

ApiSettings apiSettings = config.GetRequiredSection("ApiSettings").Get<ApiSettings>();

// Application code should start here.

// Fetching data from API
string jsonStudents = FetchFromApi.GetStudents(apiSettings);
string jsonGroups = FetchFromApi.GetGroups(apiSettings);
string jsonGroupsExtended = FetchFromApi.GetGroupsExtended(apiSettings);
string jsonStaffMembers = FetchFromApi.GetStaffMembers(apiSettings);

//Deserializing json to response object
var studentResponse = JsonSerializer.Deserialize<StudentsApiResponse>(jsonStudents);
var groupResponse = JsonSerializer.Deserialize<GroupsApiResponse>(jsonGroups);
var groupResponseExtended = JsonSerializer.Deserialize<GroupsApiResponse>(jsonGroupsExtended);
var staffResponse = JsonSerializer.Deserialize<StaffMembersApiResponse>(jsonStaffMembers);

//Saving json to file
string fileName = "Students.json";
File.WriteAllText(fileName, jsonStudents);

string fileName2 = "Groups.json";
File.WriteAllText(fileName2, jsonGroupsExtended);

string fileName3 = "StaffMembers.json";
File.WriteAllText(fileName3, jsonStaffMembers);

// Database manager
DbManager.StudentManager(studentResponse);
DbManager.CourseManager(groupResponse);
DbManager.StaffManager(staffResponse);
DbManager.ProgramManager(groupResponse);
DbManager.RelationshipManager(groupResponse, studentResponse);

await host.RunAsync();