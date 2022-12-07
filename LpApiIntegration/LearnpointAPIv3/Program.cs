using LearnpointAPIv3;
using LearnpointAPIv3.API;
using LpApiIntegration.Db;
using LpApiIntegration.FetchFromV2.Db;
using LpApiIntegration.FetchFromV3.API;
using LpApiIntegration.FetchFromV3.API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

using IHost host = Host.CreateDefaultBuilder(args).Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

ApiSettings apiSettings = config.GetRequiredSection("ApiSettings").Get<ApiSettings>();

// Application code should start here.

// Fetching data from API
string jsonCourseDefinitions = FetchFromApi.GetCourseDefinitions(apiSettings);
string jsonCourseEnrollments = FetchFromApi.GetCourseEnrollments(apiSettings);
string jsonCourseGrades = FetchFromApi.GetCourseGrades(apiSettings);
string jsonCourseInstances = FetchFromApi.GetCourseInstances(apiSettings);

string jsonGroupMemberships = FetchFromApi.GetGroupMemberships(apiSettings);
string jsonGroups = FetchFromApi.GetGroups(apiSettings);

string jsonActiveStudent = FetchFromApi.GetActiveStudents(apiSettings);
string jsonActiveStaffMembers = FetchFromApi.GetActiveStaff(apiSettings);

//Deserializing json to response object
var courseDefinitionResponse = JsonSerializer.Deserialize<CourseDefinitionListApiResponse>(jsonCourseDefinitions);
var courseEnrollmentResponse = JsonSerializer.Deserialize<CourseEnrollmentListApiResponse>(jsonCourseEnrollments);
var courseGradeResponse = JsonSerializer.Deserialize<CourseGradeListApiResponse>(jsonCourseGrades);
var courseInstanceResponse = JsonSerializer.Deserialize<CourseInstanceListApiResponse>(jsonCourseInstances);

var groupMembershipResponse = JsonSerializer.Deserialize<GroupMembershipListApiResponse>(jsonGroupMemberships);
var groupResponse = JsonSerializer.Deserialize<GroupListApiResponse>(jsonGroups);

var activeStudentsResponse = JsonSerializer.Deserialize<UserListApiResponse>(jsonActiveStudent);
var activeStaffMembersResponse = JsonSerializer.Deserialize<UserListApiResponse>(jsonActiveStaffMembers);

//Saving json to file for testing and comparasion
//File.WriteAllText("Coursedefinitions.json", jsonCourseDefinitions);
//File.WriteAllText("CourseEnrollments.json", jsonCourseEnrollments);
//File.WriteAllText("CourseGrades.json", jsonCourseGrades);
//File.WriteAllText("CourseInstances.json", jsonCourseInstances);

//File.WriteAllText("GroupMemberships.json", jsonGroupMemberships);
//File.WriteAllText("Groups.json", jsonGroups);

//File.WriteAllText("ActiveStudents.json", jsonActiveStudent);
//File.WriteAllText("ActiveStaffMembers.json", jsonActiveStaffMembers);

// Sends first batch to Database Manager then checks NextLink for further batches to send
NextLinkHandler.Students(apiSettings, activeStudentsResponse);
//DbManager.CourseManager(courseDefinitionResponse, courseInstanceResponse);
//DbManager.StaffManager(activeStaffMembersResponse);
//DbManager.ProgramManager(groupResponseExtended);
//DbManager.RelationshipManager(groupResponseExtended, groupResponse, studentResponse);

await host.RunAsync();