using LpApiIntegration.FetchFromV2.Db.Models;
using LpApiIntegration.FetchFromV2.EducationModels;
using LpApiIntegration.FetchFromV2.GroupModel;
using LpApiIntegration.FetchFromV2.StaffMemberModels;
using LpApiIntegration.FetchFromV2.StudentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public static void CourseManager(GroupsApiResponse groupsResponseExtended)
        {
            //Search for courses with category-code "CourseInstance"
            var apiCourses = groupsResponseExtended.Data.Groups.Where(c => c.Category.Code == "CourseInstance");

            //Search coursedefinitions
            var courseDefinitions = groupsResponseExtended.Data.ReferenceData.CourseDefinitions;


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

                if (DbContext.Programs.Any(c => c.ExternalId == apiProgram.Id))
                {
                    DbWorker.UpdateProgram(apiProgram, DbContext);
                }
                else
                {
                    DbWorker.AddProgram(apiProgram, DbContext);
                }
            }
            DbContext.SaveChanges();
        }

        public static void RelationshipManager(GroupsApiResponse groupResponseExtended, StudentsApiResponse studentResponse)
        {
            //Add Student-Program-Relations

            var apiPrograms = groupResponseExtended.Data.Groups.Where(p => p.Category.Code == "EducationInstance")
            .Where(p => p.StudentGroupMembers.Length > 0);

            var apiStudents = studentResponse.Data.Students;

            bool isActiveStudent = true;
            string stateName = "";
            DateTime fromDate = DateTime.Now;

            foreach (var apiProgram in apiPrograms)
            {
                var programId = DbContext.Programs.Where(p => p.ExternalId == apiProgram.Id).FirstOrDefault().Id;

                foreach (var student in apiProgram.StudentGroupMembers)
                {
                    if (DbContext.Students.Any(s => s.ExternalId == student.Student.Id))
                    {
                        var studentIdDb = DbContext.Students.Where(s => s.ExternalId == student.Student.Id).SingleOrDefault().Id;

                        if (!DbContext.StudentProgramRelations.Any(sp => Convert.ToInt32(sp.StudentId.ToString() + sp.ProgramId.ToString())
                        == Convert.ToInt32(studentIdDb.ToString() + programId.ToString())))
                        {
                            var apiStudent = studentResponse.Data.Students.Where(i => i.Id == student.Student.Id).SingleOrDefault();

                            foreach (var educationPlan in apiStudent.EducationPlans)
                            {
                                ProgramModel program = null;

                                foreach (var part in educationPlan.Parts)
                                {
                                    if (DbContext.Programs.Any(p => p.Code == part.Code) && educationPlan.State.Name != null && educationPlan.State.FromDate != null)
                                    {
                                        program = DbContext.Programs.Where(p => p.Code == part.Code).SingleOrDefault();
                                        isActiveStudent = educationPlan.State.IsActiveStudent;
                                        stateName = educationPlan.State.Name;
                                        fromDate = (DateTime)educationPlan.State.FromDate;

                                        var relation = new StudentProgramRelationModel()
                                        {
                                            StudentId = studentIdDb,
                                            ProgramId = program.Id,
                                            IsActiveStudent = isActiveStudent,
                                            StateName = stateName,
                                            FromDate = fromDate
                                        };

                                        DbWorker.AddStudentProgramRelation(relation, DbContext);
                                    }
                                }
                            }
                        }
                    }
                }

                DbContext.SaveChanges();
            }
        }
    }
}
