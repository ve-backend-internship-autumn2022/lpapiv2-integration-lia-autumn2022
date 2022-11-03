using LpApiIntegration.FetchFromV2.GroupModel;
using LpApiIntegration.FetchFromV2.StudentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.Db
{
    internal class DbManager
    {
        private static LearnpointDbContext DbContext = new();

        public static void StudentManager(StudentsApiResponse studentResponse)
        {
            foreach (var apiStudent in studentResponse.Data.Students)
            {
                if (!DbContext.Students.Any(s => s.Id == apiStudent.Id))
                {
                    DbWorker.AddStudent(apiStudent, DbContext);
                }
                else
                {
                    DbWorker.UpdateStudent(apiStudent, DbContext);
                }
                DbContext.SaveChanges();
            }
        }

        public static void CourseManager(StudentsApiResponse studentResponse, GroupsApiResponse groupsResponse)
        {
            var apiGroups = studentResponse.Data.ReferenceData.Groups.Where(c => c.Category.Name == "Kurs");
            var apiGroups2 = groupsResponse.Data.Groups.Where(i => i.Id == 312);
            var coursePoints = groupsResponse.Data.ReferenceData.CourseDefinitions.Where(n => n.IsInternship == false);

            foreach (var apiGroup in studentResponse.Data.ReferenceData.Groups)
            {
                if (!DbContext.Courses.Any(c => c.Id == apiGroup.Id))
                {
                    DbWorker.AddCourse(apiGroups, apiGroups2, coursePoints, DbContext);
                }
                //else
                //{
                //    DbWorker.UpdateStudent(apiStudent, DbContext);
                //}
                DbContext.SaveChanges();
            }
        }
    }
}
