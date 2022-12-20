using LearnpointAPIv3;
using LearnpointAPIv3.API;
using LpApiIntegration.FetchFromV2.Db;
using LpApiIntegration.FetchFromV3.API.Models;

namespace LpApiIntegration.FetchFromV3.Functions
{
    internal class FetchData
    {
        public static List<User> GetEnrollmentStudents(List<ProgramEnrollment> programEnrollments, ApiSettings apiSettings, LearnpointDbContext dbContext)
        {
            var relations = new List<ProgramEnrollment>();
            var userIdslist = new List<int>();

            foreach (var enrollment in programEnrollments)
            {
                var dbProgram = dbContext.Programs.Where(p => p.ExternalId == enrollment.ProgramInstanceId).SingleOrDefault();

                if (enrollment.ProgramInstanceId == dbProgram.ExternalId)
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
            return FetchFromApi.GetEnrollmentStudents(apiSettings, userIds.Ids);
        }
    }
}
