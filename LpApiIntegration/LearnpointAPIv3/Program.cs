using LearnpointAPIv3;
using LearnpointAPIv3.API;
using LpApiIntegration.FetchFromV2.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args).Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

ApiSettings apiSettings = config.GetRequiredSection("ApiSettings").Get<ApiSettings>();

// Application code should start here.

// Fetching data from API
var courseDefinitionList = FetchFromApi.GetCourseDefinitions(apiSettings);
var courseEnrollmentList = FetchFromApi.GetCourseEnrollments(apiSettings);

var courseInstanceList = FetchFromApi.GetCourseInstances(apiSettings);
var courseStaffMembershipList = FetchFromApi.GetCourseStaffMembership(apiSettings);

var activeStudentList = FetchFromApi.GetActiveStudents(apiSettings);
var activeStaffMemberList = FetchFromApi.GetActiveStaff(apiSettings);

var programInstanceList = FetchFromApi.GetProgramInstances(apiSettings);
var programEnrollmentList = FetchFromApi.GetProgramEnrollments(apiSettings);

DbManager.ProgramManager(programInstanceList);
DbManager.StudentManager(activeStudentList, programEnrollmentList, apiSettings);
DbManager.StaffManager(activeStaffMemberList);
DbManager.CourseManager(courseDefinitionList, courseInstanceList);
DbManager.RelationshipManager(courseStaffMembershipList, courseInstanceList,
    courseEnrollmentList, programEnrollmentList, apiSettings);

await host.RunAsync();