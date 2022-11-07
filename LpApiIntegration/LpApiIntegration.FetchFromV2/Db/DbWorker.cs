using LpApiIntegration.FetchFromV2.StudentModels;
using LpApiIntegration.FetchFromV2.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LpApiIntegration.FetchFromV2.GroupModel;
using LpApiIntegration.FetchFromV2.CourseModels;

namespace LpApiIntegration.FetchFromV2.Db
{
    internal class DbWorker
    {
        public static void AddStudent(FullStudent apiStudent, LearnpointDbContext dbContext)
        {
            dbContext.Students.Add(
            new StudentModel()
            {
                ExternalId = apiStudent.Id,
                NationalRegistrationNumber = apiStudent.NationalRegistrationNumber,
                Username = apiStudent.Username,
                Email = apiStudent.Email,
                Email2 = apiStudent.Email2,
                MobilePhone = apiStudent.MobilePhone,
                HomePhone = apiStudent.HomePhone,
                FullName = apiStudent.FirstName + " " + apiStudent.LastName,
                IsActive = true
            });
        }

        public static void UpdateStudent(FullStudent apiStudent, LearnpointDbContext dbContext)
        {
            var dbStudents = dbContext.Students;
            foreach (var dbStudent in dbStudents)
            {
                if (dbStudent.ExternalId == apiStudent.Id)
                {
                    if (dbStudent.NationalRegistrationNumber != apiStudent.NationalRegistrationNumber)
                    {
                        dbStudent.NationalRegistrationNumber = apiStudent.NationalRegistrationNumber;
                    }
                    if (dbStudent.Username != apiStudent.Username)
                    {
                        dbStudent.Username = apiStudent.Username;
                    }
                    if (dbStudent.Email != apiStudent.Email)
                    {
                        dbStudent.Email = apiStudent.Email;
                    }
                    if (dbStudent.Email2 != apiStudent.Email2)
                    {
                        dbStudent.Email2 = apiStudent.Email2;
                    }
                    if (dbStudent.MobilePhone != apiStudent.MobilePhone)
                    {
                        dbStudent.MobilePhone = apiStudent.MobilePhone;
                    }
                    if (dbStudent.HomePhone != apiStudent.HomePhone)
                    {
                        dbStudent.HomePhone = apiStudent.HomePhone;
                    }
                    if (dbStudent.FullName != apiStudent.FirstName + " " + apiStudent.LastName)
                    {
                        dbStudent.FullName = apiStudent.FirstName + " " + apiStudent.LastName;
                    }
                    if (dbStudent.IsActive == false)
                    {
                        dbStudent.IsActive = true;
                    }
                }
            }
        }
        public static void CheckForInactiveStudents(FullStudent[] students, LearnpointDbContext dbContext)
        {
            HashSet<int> diffids = new HashSet<int>(students.Select(s => s.Id));
            var results = dbContext.Students.Where(s => !diffids.Contains(s.ExternalId)).ToList();

            foreach (var student in results)
            {
                student.IsActive = false;
            }
        }

        public static void AddCourse(IEnumerable<FullGroup> apiCourses, IEnumerable<CourseDefinition> courseDefinitions, LearnpointDbContext dbContext)
        {
            foreach (var group in apiCourses)
            {
                dbContext.Courses.Add(
                   new CourseModel()
                   {
                       ExternalId = group.Id,
                       Name = group.Name,
                       Code = group.Code,
                       LifespanFrom = group.LifespanFrom,
                       LifespanUntil = group.LifespanUntil,
                       Points = courseDefinitions.Where(c => c.Id == group.CourseDefinition.Id).ToList().SingleOrDefault()?.Points
                   });
            }            
        }

        public static void AddCourseStudentRelation (GroupsApiResponse groupResponse, LearnpointDbContext DbContext)
        {
            var result = groupResponse.Data.Groups.Where(c => c.Category.Code == "CourseInstance");

            foreach (var course in result)
            {
                var courseId = DbContext.Courses.Where(i => i.ExternalId == course.Id).SingleOrDefault().Id;

                foreach (var student in course.StudentGroupMembers)
                {
                    foreach (var student2 in DbContext.Students)
                    {
                        if (student.Student.Id == student2.ExternalId)
                        {
                            DbContext.StudentCourseRelations.Add(
                                     new StudentCourseRelationModel()
                                     {
                                         StudentId = student2.Id,
                                         CourseId = courseId
                                     });
                        }
                    }
                }
            }
        }
    }
}
