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
<<<<<<< HEAD
<<<<<<< HEAD
DbManager.ProgramManager(groupResponseExtended);
DbManager.RelationshipManager(groupResponseExtended, groupResponse, studentResponse);
=======
DbManager.ProgramManager(groupResponse);
DbManager.RelationshipManager(groupResponse, studentResponse);
>>>>>>> 2789acbf07b039538f7cf6066fdb778601d7d099
=======
DbManager.ProgramManager(groupResponse);
DbManager.RelationshipManager(groupResponse, studentResponse);
>>>>>>> 2789acbf07b039538f7cf6066fdb778601d7d099

await host.RunAsync();