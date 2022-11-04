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
                if (!DbContext.Students.Any(s => s.ExternalId == apiStudent.Id))
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

        public static void CourseManager(GroupsApiResponse groupsResponse)
        {
            //Search for courses with category-code "CourseInstance"
            var apiCourses = groupsResponse.Data.Groups.Where(c => c.Category.Code == "CourseInstance");

            //Search coursedefinitions
            var courseDefinitions = groupsResponse.Data.ReferenceData.CourseDefinitions;
                       

            foreach (var apiGroup in apiCourses)
            {
                if (!DbContext.Courses.Any(c => c.ExternalId == apiGroup.Id))
                {
                    DbWorker.AddCourse(apiCourses, courseDefinitions, DbContext);
                }
                //else
                //{
                //    DbWorker.UpdateCourse(apiCourses, DbContext);
                //}
                DbContext.SaveChanges();
            }
        }        
    }
}
