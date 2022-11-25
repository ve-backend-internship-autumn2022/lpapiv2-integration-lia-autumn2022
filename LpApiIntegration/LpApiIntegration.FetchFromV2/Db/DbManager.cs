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

        public static void ProgramManager(GroupsApiResponse groupResponse)
        {
            var apiPrograms = groupResponse.Data.Groups.Where(p => p.Category.Code == "EducationInstance");

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

        public static void RelationshipManager(GroupsApiResponse groupReponseExtended, GroupsApiResponse groupResponse, StudentsApiResponse studentResponse)
        {
            //Add and delete Student-course-relations

            var studentCourses = groupResponse.Data.Groups.Where(p => p.Category.Code == "CourseInstance")
                .Where(s => s.StudentGroupMembers.Length > 0);

            foreach (var course in studentCourses)
            {
                var courseId = DbContext.Courses.Where(c => c.ExternalId == course.Id).FirstOrDefault().Id;

                foreach (var current in course.StudentGroupMembers)
                {
                    if (DbContext.StaffMembers.Any(s => s.ExternalId == current.Student.Id))
                    {
                        var studentDbId = DbContext.Students.Where(s => s.ExternalId == current.Student.Id).SingleOrDefault().Id;

                        if (!DbContext.StudentCourseRelations.Any(sp => Convert.ToInt32(sp.StudentId.ToString() + sp.CourseId.ToString())
                        == Convert.ToInt32(studentDbId.ToString() + courseId.ToString())))
                        {
                            var relation = new StudentCourseRelationModel()
                            {
                                StudentId = studentDbId,
                                CourseId = courseId
                            };

                            DbWorker.AddCourseStudentRelation(relation, DbContext);
                        }
                        else
                        {

                            foreach (var Course in studentCourses)
                            {
                                if (Course.Id == 320)
                                {
                                    var studentGroupMembers = Course.StaffGroupMembers.ToList();

                                    studentGroupMembers.RemoveAt(0);

                                    //Hämta eget kurs-id för aktuell kurs
                                    var CourseId = DbContext.Courses.Where(c => c.ExternalId == Course.Id).SingleOrDefault().Id;

                                    //Lägg alla id:n i StaffGroupMembers i en HashSet
                                    HashSet<int> apiIds = new HashSet<int>(studentGroupMembers.Select(s => s.StaffMember.Id));

                                    //Lägg alla externa id:n som har en relation med aktuell kurs i en Hashset
                                    HashSet<int> dbIds = new HashSet<int>(DbContext.StudentCourseRelations.Where(c => c.CourseId == CourseId).Include(p => p.Student)
                                        .Select(s => s.Student.ExternalId));

                                    //Gör en lista som visar vilka id:n som finns i databasen men inte i StaffGroupMembers
                                    var idDifferences = dbIds.Except(apiIds).ToList();

                                    //Om listan inte är tom
                                    if (idDifferences.Count != 0)
                                    {
                                        //Loopa igenom listan
                                        foreach (var externalId in idDifferences)
                                        {
                                            //Ta fram eget StaffId för varje externalId i listan
                                            var studentId = DbContext.Students.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

                                            //Ta fram relationen som StaffId:t har med kursen
                                            var relation = DbContext.StudentCourseRelations.Where(s => s.StudentId == studentId)
                                                .Where(c => c.CourseId == CourseId).SingleOrDefault();

                                            //Ta bort relationen
                                            DbContext.StudentCourseRelations.Remove(relation);

                                        }
                                    }
                                }
                                else
                                {
                                    //Hämta eget kurs-id för aktuell kurs
                                    var CourseId = DbContext.Courses.Where(c => c.ExternalId == Course.Id).SingleOrDefault().Id;

                                    //Lägg alla id:n i StaffGroupMembers i en HashSet
                                    HashSet<int> apiIds = new HashSet<int>(Course.StudentGroupMembers.Select(s => s.Student.Id));

                                    //Lägg alla externa id:n som har en relation med aktuell kurs i en Hashset
                                    HashSet<int> dbIds = new HashSet<int>(DbContext.StudentCourseRelations.Where(c => c.CourseId == CourseId).Include(p => p.Student)
                                        .Select(s => s.Student.ExternalId));

                                    //Gör en lista som visar vilka id:n som finns i databasen men inte i StaffGroupMembers
                                    var idDifferences = dbIds.Except(apiIds).ToList();

                                    //Om listan inte är tom
                                    if (idDifferences.Count != 0)
                                    {
                                        //Loopa igenom listan
                                        foreach (var externalId in idDifferences)
                                        {
                                            //Ta fram eget StaffId för varje externalId i listan
                                            var studentId = DbContext.Students.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

                                            //Ta fram relationen som StaffId:t har med kursen
                                            var relation = DbContext.StudentCourseRelations.Where(s => s.StudentId == studentId)
                                                .Where(c => c.CourseId == CourseId).SingleOrDefault();

                                            //Ta bort relationen
                                            DbContext.StudentCourseRelations.Remove(relation);

                                        }
                                    }
                                }

                            }

                        }
                    }
                }
            }
            //Add and delete staff-course-relations

            var courses = groupResponse.Data.Groups.Where(p => p.Category.Code == "CourseInstance")
                .Where(p => p.StaffGroupMembers.Length > 0);

            foreach (var course in courses)
            {
                var courseId = DbContext.Courses.Where(p => p.ExternalId == course.Id).FirstOrDefault().Id;

                foreach (var staff in course.StaffGroupMembers)
                {
                    if (DbContext.StaffMembers.Any(s => s.ExternalId == staff.StaffMember.Id))
                    {
                        var staffIdDb = DbContext.StaffMembers.Where(s => s.ExternalId == staff.StaffMember.Id).SingleOrDefault().Id;

                        if (!DbContext.StaffCourseRelations.Any(sp => Convert.ToInt32(sp.StaffMemberId.ToString() + sp.CourseId.ToString())
                        == Convert.ToInt32(staffIdDb.ToString() + courseId.ToString())))
                        {
                            var relation = new StaffCourseRelationModel()
                            {
                                StaffMemberId = staffIdDb,
                                CourseId = courseId
                            };

                            DbWorker.AddCourseStaffRelation(relation, DbContext);
                        }
                        else
                        {

                            foreach (var Course in courses)
                            {
                                if (Course.Id == 320)
                                {
                                    var staffGroupMembers = course.StaffGroupMembers.ToList();

                                    staffGroupMembers.RemoveAt(0);

                                    //Hämta eget kurs-id för aktuell kurs
                                    var CourseId = DbContext.Courses.Where(c => c.ExternalId == Course.Id).SingleOrDefault().Id;

                                    //Lägg alla id:n i StaffGroupMembers i en HashSet
                                    HashSet<int> apiIds = new HashSet<int>(staffGroupMembers.Select(s => s.StaffMember.Id));

                                    //Lägg alla externa id:n som har en relation med aktuell kurs i en Hashset
                                    HashSet<int> dbIds = new HashSet<int>(DbContext.StaffCourseRelations.Where(c => c.CourseId == CourseId).Include(p => p.StaffMember)
                                        .Select(s => s.StaffMember.ExternalId));

                                    //Gör en lista som visar vilka id:n som finns i databasen men inte i StaffGroupMembers
                                    var idDifferences = dbIds.Except(apiIds).ToList();

                                    //Om listan inte är tom
                                    if (idDifferences.Count != 0)
                                    {
                                        //Loopa igenom listan
                                        foreach (var externalId in idDifferences)
                                        {
                                            //Ta fram eget StaffId för varje externalId i listan
                                            var staffMemberId = DbContext.StaffMembers.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

                                            //Ta fram relationen som StaffId:t har med kursen
                                            var relation = DbContext.StaffCourseRelations.Where(s => s.StaffMemberId == staffMemberId)
                                                .Where(c => c.CourseId == CourseId).SingleOrDefault();

                                            //Ta bort relationen
                                            DbContext.StaffCourseRelations.Remove(relation);

                                        }
                                    }
                                }
                                else
                                {
                                    //Hämta eget kurs-id för aktuell kurs
                                    var dbCourseId = DbContext.Courses.Where(c => c.ExternalId == Course.Id).SingleOrDefault().Id;

                                    //Lägg alla id:n i StaffGroupMembers i en HashSet                                    
                                    HashSet<int> apiIds = new HashSet<int>(Course.StaffGroupMembers.Select(s => s.StaffMember.Id));

                                    //Lägg alla externa id:n som har en relation med aktuell kurs i en Hashset
                                    HashSet<int> dbIds = new HashSet<int>(DbContext.StaffCourseRelations.Where(c => c.CourseId == dbCourseId).Include(p => p.StaffMember)
                                        .Select(s => s.StaffMember.ExternalId));

                                    //Gör en lista som visar vilka id:n som finns i databasen men inte i StaffGroupMembers
                                    var idDifferences = dbIds.Except(apiIds).ToList();

                                    //Om listan inte är tom
                                    if (idDifferences.Count != 0)
                                    {
                                        //Loopa igenom listan
                                        foreach (var externalId in idDifferences)
                                        {
                                            //Ta fram eget StaffId för varje externalId i listan
                                            var staffMemberId = DbContext.StaffMembers.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

                                            //Ta fram relationen som StaffId:t har med kursen
                                            var relation = DbContext.StaffCourseRelations.Where(s => s.StaffMemberId == staffMemberId)
                                                .Where(c => c.CourseId == dbCourseId).SingleOrDefault();

                                            //Ta bort relationen
                                            DbContext.StaffCourseRelations.Remove(relation);

                                        }
                                    }
                                }

                            }

                        }
                    }
                }
            }

            DbContext.SaveChanges();


            //Add and delete student-program-relations
            var apiPrograms = groupReponseExtended.Data.Groups.Where(p => p.Category.Code == "EducationInstance")
            .Where(p => p.StudentGroupMembers.Length > 0);

            var apiStudents = studentResponse.Data.Students;

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

                                        var relation = new StudentProgramRelationModel()
                                        {
                                            StudentId = studentIdDb,
                                            ProgramId = program.Id,
                                            IsActiveStudent = educationPlan.State.IsActiveStudent,
                                            StateName = educationPlan.State.Name,
                                            FromDate = (DateTime)educationPlan.State.FromDate
                                        };

                                        DbWorker.AddStudentProgramRelation(relation, DbContext);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var program in apiPrograms)
                            {
                                if (program.Id == 322)
                                {
                                    var studentGroupMembers = program.StudentGroupMembers.ToList();

                                    studentGroupMembers.RemoveAt(0);

                                    //Hämta eget program-id för aktuell kurs
                                    var dbProgramId = DbContext.Programs.Where(c => c.ExternalId == program.Id).SingleOrDefault().Id;

                                    //Lägg alla id:n i StudentGroupMembers i en HashSet                                    
                                    HashSet<int> apiIds = new HashSet<int>(studentGroupMembers.Select(s => s.Student.Id));

                                    //Lägg alla externa id:n som har en relation med aktuellt program i en Hashset
                                    HashSet<int> dbIds = new HashSet<int>(DbContext.StudentProgramRelations.Where(c => c.ProgramId == dbProgramId).Include(p => p.Student)
                                        .Select(s => s.Student.ExternalId));

                                    //Gör en lista som visar vilka id:n som finns i databasen men inte i StudentGroupMembers
                                    var idDifferences = dbIds.Except(apiIds).ToList();

                                    //Om listan inte är tom
                                    if (idDifferences.Count != 0)
                                    {
                                        //Loopa igenom listan
                                        foreach (var externalId in idDifferences)
                                        {
                                            //Ta fram eget studentId för varje externalId i listan
                                            var studentId = DbContext.Students.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

                                            //Ta fram relationen som student-id:t har med programmet
                                            var relation = DbContext.StudentProgramRelations.Where(s => s.StudentId == studentId)
                                                .Where(c => c.ProgramId == dbProgramId).SingleOrDefault();

                                            //Ta bort relationen
                                            DbContext.StudentProgramRelations.Remove(relation);

                                        }
                                    }
                                }
                                else
                                {
                                    //Hämta eget program-id för aktuellt program
                                    var dbProgramId = DbContext.Programs.Where(c => c.ExternalId == program.Id).SingleOrDefault().Id;

                                    //Lägg alla id:n i StudentGroupMembers i en HashSet                                    
                                    HashSet<int> apiIds = new HashSet<int>(program.StudentGroupMembers.Select(s => s.Student.Id));

                                    //Lägg alla externa id:n som har en relation med aktuellt program i en Hashset
                                    HashSet<int> dbIds = new HashSet<int>(DbContext.StudentProgramRelations.Where(c => c.ProgramId == dbProgramId).Include(p => p.Student)
                                        .Select(s => s.Student.ExternalId));

                                    //Gör en lista som visar vilka id:n som finns i databasen men inte i StudentGroupMembers
                                    var idDifferences = dbIds.Except(apiIds).ToList();

                                    //Om listan inte är tom
                                    if (idDifferences.Count != 0)
                                    {
                                        //Loopa igenom listan
                                        foreach (var externalId in idDifferences)
                                        {
                                            //Ta fram eget studentId för varje externalId i listan
                                            var studentId = DbContext.Students.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

                                            //Ta fram relationen som student-id:t har med programmet
                                            var relation = DbContext.StudentProgramRelations.Where(s => s.StudentId == studentId)
                                                .Where(c => c.ProgramId == dbProgramId).SingleOrDefault();

                                            //Ta bort relationen
                                            DbContext.StudentProgramRelations.Remove(relation);

                                        }
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
