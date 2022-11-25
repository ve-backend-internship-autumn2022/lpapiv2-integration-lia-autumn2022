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
using System.Xml.Linq;
using LpApiIntegration.FetchFromV2.StaffMemberModels;

namespace LpApiIntegration.FetchFromV2.Db
{
    internal class DbWorker
    {
        // Student
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

        // Course

        public static void AddCourse(FullGroup apiCourse, IEnumerable<CourseDefinition> courseDefinitions, LearnpointDbContext dbContext)
        {
            dbContext.Courses.Add(
                   new CourseModel()
                   {
                       ExternalId = apiCourse.Id,
                       Name = apiCourse.Name,
                       Code = apiCourse.Code,
                       LifespanFrom = apiCourse.LifespanFrom,
                       LifespanUntil = apiCourse.LifespanUntil,
                       Points = courseDefinitions.Where(c => c.Id == apiCourse.CourseDefinition.Id).ToList().SingleOrDefault()?.Points
                   });
        }

        public static void UpdateCourse(FullGroup apiCourse, IEnumerable<CourseDefinition> courseDefinitions, LearnpointDbContext dbContext)
        {
            var courseDB = dbContext.Courses;

            foreach (var course in courseDB)
            {
                if (course.ExternalId == apiCourse.Id)
                {
                    if (course.Name != apiCourse.Name)
                    {
                        course.Name = apiCourse.Name;
                    }
                    if (course.Code != apiCourse.Code)
                    {
                        course.Code = apiCourse.Code;
                    }
                    if (course.LifespanFrom != apiCourse.LifespanFrom)
                    {
                        course.LifespanFrom = apiCourse.LifespanFrom;
                    }
                    if (course.LifespanUntil != apiCourse.LifespanUntil)
                    {
                        course.LifespanUntil = apiCourse.LifespanUntil;
                    }
                    if (course.Points != courseDefinitions.Where(c => c.Id == apiCourse.CourseDefinition.Id).ToList().SingleOrDefault()?.Points)
                    {
                        course.Points = courseDefinitions.Where(c => c.Id == apiCourse.CourseDefinition.Id).ToList().SingleOrDefault()?.Points;
                    }
                }
            }
        }

        // Staff

        public static void AddStaffMember(FullStaffMember apiStaffMember, LearnpointDbContext dbContext)
        {
            dbContext.StaffMembers.Add(
            new StaffModel()
            {
                ExternalId = apiStaffMember.Id,
                NationalRegistrationNumber = apiStaffMember.NationalRegistrationNumber,
                Signature = apiStaffMember.Signature,
                FullName = apiStaffMember.FirstName + " " + apiStaffMember.LastName,
                Username = apiStaffMember.Username,
                Email = apiStaffMember.Email,
                Email2 = apiStaffMember.Email2,
                MobilePhone = apiStaffMember.MobilePhone,
                MayExposeMobilePhoneToStudents = apiStaffMember.MayExposeMobilePhoneToStudents,
                Phone2 = apiStaffMember.Phone2,
                MayExposePhone2ToStudents = apiStaffMember.MayExposePhone2ToStudents,

            });
        }

        public static void UpdateStaffMember(FullStaffMember apiStaffMember, LearnpointDbContext dbContext)
        {
            var dbStaffMembers = dbContext.StaffMembers;
            foreach (var dbStaffMember in dbStaffMembers)
            {
                if (dbStaffMember.ExternalId == apiStaffMember.Id)
                {
                    if (dbStaffMember.NationalRegistrationNumber != apiStaffMember.NationalRegistrationNumber)
                    {
                        dbStaffMember.NationalRegistrationNumber = apiStaffMember.NationalRegistrationNumber;
                    }
                    if (dbStaffMember.Signature != apiStaffMember.Signature)
                    {
                        dbStaffMember.Signature = apiStaffMember.Signature;
                    }
                    if (dbStaffMember.FullName != apiStaffMember.FirstName + " " + apiStaffMember.LastName)
                    {
                        dbStaffMember.FullName = apiStaffMember.FirstName + " " + apiStaffMember.LastName;
                    }
                    if (dbStaffMember.Username != apiStaffMember.Username)
                    {
                        dbStaffMember.Username = apiStaffMember.Username;
                    }
                    if (dbStaffMember.Email != apiStaffMember.Email)
                    {
                        dbStaffMember.Email = apiStaffMember.Email;
                    }
                    if (dbStaffMember.Email2 != apiStaffMember.Email2)
                    {
                        dbStaffMember.Email2 = apiStaffMember.Email2;
                    }
                    if (dbStaffMember.MobilePhone != apiStaffMember.MobilePhone)
                    {
                        dbStaffMember.MobilePhone = apiStaffMember.MobilePhone;
                    }
                    if (dbStaffMember.MayExposeMobilePhoneToStudents != apiStaffMember.MayExposeMobilePhoneToStudents)
                    {
                        dbStaffMember.MayExposeMobilePhoneToStudents = apiStaffMember.MayExposeMobilePhoneToStudents;
                    }
                    if (dbStaffMember.Phone2 != apiStaffMember.Phone2)
                    {
                        dbStaffMember.Phone2 = apiStaffMember.Phone2;
                    }
                    if (dbStaffMember.MayExposePhone2ToStudents != apiStaffMember.MayExposePhone2ToStudents)
                    {
                        dbStaffMember.MayExposePhone2ToStudents = apiStaffMember.MayExposePhone2ToStudents;
                    }
                }
            }
        }

        // Program

        public static void AddProgram(FullGroup apiProgram, LearnpointDbContext dbContext)
        {
            dbContext.Programs.Add(
                new ProgramModel()
                {
                    ExternalId = apiProgram.Id,
                    Code = apiProgram.Code,
                    Name = apiProgram.Name,
                    LifespanFrom = apiProgram.LifespanFrom,
                    LifespanUntil = apiProgram.LifespanUntil
                });
        }
        public static void UpdateProgram(FullGroup apiProgram, LearnpointDbContext dbContext)
        {
            var dbProgram = dbContext.Programs.Where(e => e.ExternalId == apiProgram.Id).SingleOrDefault();

            if (dbProgram.Code != apiProgram.Code)
            {
                dbProgram.Code = apiProgram.Code;
            }
            if (dbProgram.Name != apiProgram.Name)
            {
                dbProgram.Name = apiProgram.Name;
            }
            if (dbProgram.LifespanFrom != apiProgram.LifespanFrom)
            {
                dbProgram.LifespanFrom = apiProgram.LifespanFrom;
            }
            if (dbProgram.LifespanUntil != apiProgram.LifespanUntil)
            {
                dbProgram.LifespanUntil = apiProgram.LifespanUntil;
            }

           
        }

        // Relations
        public static void AddCourseStudentRelation(StudentCourseRelationModel relation, LearnpointDbContext dbContext)
        {
            dbContext.StudentCourseRelations.Add(relation);
        }
        public static void AddCourseStaffRelation(StaffCourseRelationModel relation, LearnpointDbContext dbContext)
        {
            dbContext.StaffCourseRelations.Add(relation);
        }

        public static void AddStudentProgramRelation(StudentProgramRelationModel relation, LearnpointDbContext dbContext)
        {
             dbContext.StudentProgramRelations.Add(relation);
        }
    }
}


    

