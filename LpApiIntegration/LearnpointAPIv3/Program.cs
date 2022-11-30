using LearnpointAPIv3;
using LearnpointAPIv3.API;
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

//Deserializing json to response object
var courseDefinitionResponse = JsonSerializer.Deserialize<CourseDefinitionListApiResponse>(jsonCourseDefinitions);
var courseEnrollmentResponse = JsonSerializer.Deserialize<CourseEnrollmentListApiResponse>(jsonCourseEnrollments);
var courseGradeResponse = JsonSerializer.Deserialize<CourseGradeListApiResponse>(jsonCourseGrades);
var courseInstanceResponse = JsonSerializer.Deserialize<CourseInstanceListApiResponse>(jsonCourseInstances);

var groupMembershipResponse = JsonSerializer.Deserialize<GroupMembershipListApiResponse>(jsonGroupMemberships);
var groupResponse = JsonSerializer.Deserialize<GroupListApiResponse>(jsonGroups);


//Console.WriteLine(groupResponse.Data[0].Name);

//Saving json to file
File.WriteAllText("Coursedefinitions.json", jsonCourseDefinitions);
File.WriteAllText("CourseEnrollments.json", jsonCourseEnrollments);
File.WriteAllText("CourseGrades.json", jsonCourseGrades);
File.WriteAllText("CourseInstances.json", jsonCourseInstances);

File.WriteAllText("GroupMemberships.json", jsonGroupMemberships);
File.WriteAllText("Groups.json", jsonGroups);

// Database manager
//DbManager.StudentManager(studentResponse);
//DbManager.CourseManager(groupResponse);
//DbManager.StaffManager(staffResponse);
//DbManager.ProgramManager(groupResponseExtended);
//DbManager.RelationshipManager(groupResponseExtended, groupResponse, studentResponse);

await host.RunAsync();