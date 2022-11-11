using LpApiIntegration.FetchFromV2.Db.Models;
using LpApiIntegration.FetchFromV2.GroupModel;
using LpApiIntegration.FetchFromV2.StaffMemberModels;
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
            var apiStudents = studentResponse.Data.Students;
            foreach (var apiStudent in apiStudents)
            {
                if (!DbContext.Students.Any(s => s.ExternalId == apiStudent.Id))
                {
                    DbWorker.AddStudent(apiStudent, DbContext);
                }
                else
                {
                    DbWorker.UpdateStudent(apiStudent, DbContext);
                }
            }
            DbWorker.CheckForInactiveStudents(apiStudents, DbContext);
            DbContext.SaveChanges();
        }

        public static void CourseManager(GroupsApiResponse groupsResponse)
        {
            //Search for courses with category-code "CourseInstance"
            var apiCourses = groupsResponse.Data.Groups.Where(c => c.Category.Code == "CourseInstance");

            //Search coursedefinitions
            var courseDefinitions = groupsResponse.Data.ReferenceData.CourseDefinitions;


            foreach (var apiCourse in apiCourses)
            {
                if (!DbContext.Courses.Any(c => c.ExternalId == apiCourse.Id))
                {
                    DbWorker.AddCourse(apiCourse, courseDefinitions, DbContext);
                }
                else
                {
                    DbWorker.UpdateCourse(apiCourse, courseDefinitions, DbContext);
                }
            }
            DbContext.SaveChanges();
        }

        public static void StaffManager(StaffMembersApiResponse staffResponse)
        {
            var apiStaffMembers = staffResponse.Data.StaffMembers;
            foreach (var apiStaffMember in apiStaffMembers)
            {
                if (!DbContext.StaffMembers.Any(s => s.ExternalId == apiStaffMember.Id))
                {
                    DbWorker.AddStaffMember(apiStaffMember, DbContext);
                }
                else
                {
                    DbWorker.UpdateStaffMember(apiStaffMember, DbContext);
                }
            }

            DbContext.SaveChanges();
        }

        public static void ProgramManager(GroupsApiResponse groupResponseExtended)
        {
            var apiPrograms = groupResponseExtended.Data.Groups.Where(p => p.Category.Code == "EducationInstance");

            foreach (var apiProgram in apiPrograms)
            {
                if (!DbContext.Programs.Any(p => p.ExternalId == apiProgram.Id))
                {
                    DbWorker.AddProgram(apiProgram, DbContext);
                }
                else
                {
                    DbWorker.UpdateProgram(apiProgram, DbContext);
                }
            }
            DbContext.SaveChanges();
        }

        public static void RelationshipManager(GroupsApiResponse groupResponse, GroupsApiResponse groupResponseExtended, StudentsApiResponse studentResponse)
        {
            DbWorker.AddCourseStudentRelation(groupResponse, DbContext);
            DbWorker.AddCourseStaffRelation(groupResponse, DbContext);
            DbWorker.AddStudentProgramRelation(groupResponseExtended, studentResponse, DbContext);

            DbContext.SaveChanges();
        }

        
    }
}
