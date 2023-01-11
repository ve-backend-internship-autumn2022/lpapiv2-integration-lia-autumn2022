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
var courseGradeList = FetchFromApi.GetCourseGrades(apiSettings);

var activeStudentList = FetchFromApi.GetActiveStudents(apiSettings);
var activeStaffMemberList = FetchFromApi.GetActiveStaff(apiSettings);

var programInstanceList = FetchFromApi.GetProgramInstances(apiSettings);
var programEnrollmentList = FetchFromApi.GetProgramEnrollments(apiSettings);

DbManager.StudentManager(activeStudentList, programEnrollmentList, programInstanceList, courseGradeList, apiSettings);
DbManager.ProgramManager(programInstanceList, programEnrollmentList);
DbManager.StaffManager(activeStaffMemberList);
DbManager.CourseManager(courseDefinitionList, courseInstanceList, courseGradeList, apiSettings);
DbManager.RelationshipManager(courseStaffMembershipList, courseInstanceList,
    courseEnrollmentList, programEnrollmentList, courseGradeList, apiSettings);

await host.RunAsync();