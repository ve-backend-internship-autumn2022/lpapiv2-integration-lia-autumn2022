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
string jsonCourseStaffMembership = FetchFromApi.GetCourseStaffMembership(apiSettings);
string jsonGroups = FetchFromApi.GetGroups(apiSettings);

string jsonActiveStudent = FetchFromApi.GetActiveStudents(apiSettings);
string jsonActiveStaffMembers = FetchFromApi.GetActiveStaff(apiSettings);

string jsonProgramInstances = FetchFromApi.GetProgramInstances(apiSettings);

//Deserializing json to response object
var courseDefinitionResponse = JsonSerializer.Deserialize<CourseDefinitionListApiResponse>(jsonCourseDefinitions);
var courseEnrollmentResponse = JsonSerializer.Deserialize<CourseEnrollmentListApiResponse>(jsonCourseEnrollments);
var courseGradeResponse = JsonSerializer.Deserialize<CourseGradeListApiResponse>(jsonCourseGrades);
var courseInstanceResponse = JsonSerializer.Deserialize<CourseInstanceListApiResponse>(jsonCourseInstances);

var groupMembershipResponse = JsonSerializer.Deserialize<GroupMembershipListApiResponse>(jsonGroupMemberships);
var courseStaffMembershipResponse = JsonSerializer.Deserialize<CourseStaffMembershipListApiResponse>(jsonCourseStaffMembership);
var groupResponse = JsonSerializer.Deserialize<GroupListApiResponse>(jsonGroups);

var activeStudentsResponse = JsonSerializer.Deserialize<UserListApiResponse>(jsonActiveStudent);
var activeStaffMembersResponse = JsonSerializer.Deserialize<UserListApiResponse>(jsonActiveStaffMembers);

var programInstanceResponse = JsonSerializer.Deserialize<ProgramInstanceListApiResponse>(jsonProgramInstances);

//Saving json to file
//File.WriteAllText("CourseDefinitions.json", jsonCourseDefinitions);
//File.WriteAllText("CourseEnrollments.json", jsonCourseEnrollments);
//File.WriteAllText("CourseGrades.json", jsonCourseGrades);
//File.WriteAllText("CourseInstances.json", jsonCourseInstances);

//File.WriteAllText("GroupMemberships.json", jsonGroupMemberships);
//File.WriteAllText("Groups.json", jsonGroups);

//File.WriteAllText("ActiveStudents.json", jsonActiveStudent);
//File.WriteAllText("ActiveStaffMembers.json", jsonActiveStaffMembers);

//File.WriteAllText("ProgramInstances.json", jsonProgramInstances);

// Sends first batch to Database Manager then checks NextLink for further batches to send
NextLinkHandler.Students(apiSettings, activeStudentsResponse);
//NextLinkHandler.Courses(apiSettings, courseDefinitionResponse, courseInstanceResponse);
NextLinkHandler.StaffMembers(apiSettings, activeStaffMembersResponse);
NextLinkHandler.Programs(apiSettings, programInstanceResponse);
NextLinkHandler.Relations(apiSettings, courseStaffMembershipResponse, courseInstanceResponse);

await host.RunAsync();