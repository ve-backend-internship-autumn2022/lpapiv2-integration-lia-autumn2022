using LearnpointAPIv3;
using LearnpointAPIv3.API;
using LpApiIntegration.Db;
using LpApiIntegration.FetchFromV3.API.Models;

namespace LpApiIntegration.FetchFromV3.Functions
{
    internal class FetchData
    {
        public static List<User> GetEnrollmentStudents(List<ProgramEnrollment> programEnrollments, List<ProgramInstance> programs, ApiSettings apiSettings, LearnpointDbContext dbContext)
        {
            var relations = new List<ProgramEnrollment>();
            var userIdslist = new List<int>();

            foreach (var enrollment in programEnrollments)
            {
                var program = programs.Where(p => p.Id == enrollment.ProgramInstanceId).SingleOrDefault();

                if (enrollment.ProgramInstanceId == program.Id)
                {
                    relations.Add(enrollment);
                }
            }

            foreach (var relation in relations)
            {
                if (!dbContext.Students.Any(s => s.ExternalId == relation.UserId))
                {
                    userIdslist.Add(relation.UserId);
                }
            }

            var userIds = new { Ids = userIdslist.ToArray() };
            return FetchFromApi.GetUserLookup(apiSettings, userIds.Ids);
        }

        public static List<User> GetGradingStudents(List<CourseGrade> courseGrades, ApiSettings apiSettings, LearnpointDbContext dbContext)
        {
            var userIdslist = new List<int>();

            foreach (var grade in courseGrades)
            {
                if (!dbContext.Students.Any(s => s.ExternalId == grade.UserId))
                {
                    userIdslist.Add(grade.UserId);
                }
            }

            var userIds = new { Ids = userIdslist.ToArray() };
            return FetchFromApi.GetUserLookup(apiSettings, userIds.Ids);
        }

        public static List<CourseInstance> GetCourses(List<CourseGrade> courseGrades, ApiSettings apiSettings, LearnpointDbContext dbContext)
        {
            var courseInstanceIdlist = new List<int?>();

            foreach (var grade in courseGrades)
            {
                if (!dbContext.Courses.Any(s => s.ExternalId == grade.AwardedInCourseInstanceId))
                {
                    if (grade.AwardedInCourseInstanceId != null)
                    {
                        courseInstanceIdlist.Add(grade.AwardedInCourseInstanceId);
                    }
                    else
                    {
                        var allCourseInstances = FetchFromApi.GetCourseInstances(apiSettings);

                        if (allCourseInstances.Any(c => c.CourseDefinitionId == grade.CourseDefinitionId))
                        {
                            var awardedInCourseInstanceId = allCourseInstances.Where(c => c.CourseDefinitionId == grade.CourseDefinitionId).SingleOrDefault().Id;

                            if (awardedInCourseInstanceId != null)
                            {
                                courseInstanceIdlist.Add(awardedInCourseInstanceId);
                            }
                            else
                            {
                                courseInstanceIdlist.Add(null);
                            }
                        }
                    }
                }
            }

            var courseInstanceIds = new { Ids = courseInstanceIdlist.Distinct().ToArray() };
            return FetchFromApi.GetCourseInstancesLookup(apiSettings, courseInstanceIds.Ids);
        }
    }
}
