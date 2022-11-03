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

        public static void CourseManager(GroupsApiResponse groupsResponse)
        {
            //Sökning på grupper med kategorikod som visar att det är en kurs
            var apiCourses = groupsResponse.Data.Groups.Where(c => c.Category.Code == "CourseInstance");

            //Sökning för att få points för kurserna
            var coursePoints = groupsResponse.Data.ReferenceData.CourseDefinitions.Where(n => n.IsInternship == false);
                       

            foreach (var apiGroup in apiCourses)
            {
                if (!DbContext.Courses.Any(c => c.ExternalId == apiGroup.Id))
                {
                    DbWorker.AddCourse(apiCourses, coursePoints, DbContext);
                }
                //    //else
                //    //{
                //    //    DbWorker.UpdateStudent(apiStudent, DbContext);
                //    //}
                //    //DbContext.SaveChanges();
                //}
            }

        }
    }
}
