
using LpApiIntegration.Db;
using LpApiIntegration.FetchFromV2.GroupModel;
using LpApiIntegration.FetchFromV2.StaffMemberModels;
using LpApiIntegration.FetchFromV2.StudentModels;
using Microsoft.EntityFrameworkCore;

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
            StudentCourseRelations(groupResponse);
            StaffCourseRelation(groupResponse);
            StudentProgramRelation(groupReponseExtended, studentResponse);

            DbContext.SaveChanges();

            static void StudentCourseRelations(GroupsApiResponse groupResponse)
            {
                //Add and delete Student-course-relations

                var studentCourses = groupResponse.Data.Groups.Where(p => p.Category.Code == "CourseInstance")
                    .Where(s => s.StudentGroupMembers.Length > 0);

                foreach (var course in studentCourses)
                {
                    var courseId = DbContext.Courses.Where(c => c.ExternalId == course.Id).FirstOrDefault().Id;

                    foreach (var currentStudent in course.StudentGroupMembers)
                    {
                        if (DbContext.Students.Any(s => s.ExternalId == currentStudent.Student.Id))
                        {
                            var studentId = DbContext.Students.Where(s => s.ExternalId == currentStudent.Student.Id).SingleOrDefault().Id;

                            if (!DbContext.StudentCourseRelations.Any(sp => Convert.ToInt32(sp.StudentId.ToString() + sp.CourseId.ToString())
                            == Convert.ToInt32(studentId.ToString() + courseId.ToString())))
                            {
                                var relation = new StudentCourseRelationModel()
                                {
                                    StudentId = studentId,
                                    CourseId = courseId
                                };

                                DbWorker.AddCourseStudentRelation(relation, DbContext);
                            }
                            else
                            {
                                HashSet<int> apiIds = new(course.StudentGroupMembers.Select(s => s.Student.Id));

                                HashSet<int> dbIds = new(DbContext.StudentCourseRelations.Where(c => c.CourseId == courseId)
                                    .Include(p => p.Student)
                                    .Select(s => s.Student.ExternalId));

                                var idDifferences = dbIds.Except(apiIds).ToList();

                                if (idDifferences.Count != 0)
                                {
                                    foreach (var externalId in idDifferences)
                                    {
                                        studentId = DbContext.Students.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

                                        var relation = DbContext.StudentCourseRelations.Where(s => s.StudentId == studentId)
                                            .Where(c => c.CourseId == courseId).SingleOrDefault();

                                        DbWorker.RemoveCourseStudentRelation(relation, DbContext);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            static void StaffCourseRelation(GroupsApiResponse groupResponse)
            {
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
                            var staffId = DbContext.StaffMembers.Where(s => s.ExternalId == staff.StaffMember.Id).SingleOrDefault().Id;

                            if (!DbContext.StaffCourseRelations.Any(sp => Convert.ToInt32(sp.StaffMemberId.ToString() + sp.CourseId.ToString())
                            == Convert.ToInt32(staffId.ToString() + courseId.ToString())))
                            {
                                var relation = new StaffCourseRelationModel()
                                {
                                    StaffMemberId = staffId,
                                    CourseId = courseId
                                };

                                DbWorker.AddCourseStaffRelation(relation, DbContext);
                            }
                            else
                            {
                                HashSet<int> apiIds = new(course.StaffGroupMembers.Select(s => s.StaffMember.Id));

                                HashSet<int> dbIds = new(DbContext.StaffCourseRelations.Where(c => c.CourseId == courseId)
                                    .Include(p => p.StaffMember)
                                    .Select(s => s.StaffMember.ExternalId));

                                var idDifferences = dbIds.Except(apiIds).ToList();

                                if (idDifferences.Count != 0)
                                {
                                    foreach (var externalId in idDifferences)
                                    {
                                        staffId = DbContext.StaffMembers.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

                                        var relation = DbContext.StaffCourseRelations.Where(s => s.StaffMemberId == staffId)
                                            .Where(c => c.CourseId == courseId).SingleOrDefault();

                                        DbWorker.RemoveCourseStaffRelation(relation, DbContext);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            static void StudentProgramRelation(GroupsApiResponse groupReponseExtended, StudentsApiResponse studentResponse)
            {
                //Add and delete student-program-relations

                var apiPrograms = groupReponseExtended.Data.Groups.Where(p => p.Category.Code == "EducationInstance")
                .Where(p => p.StudentGroupMembers.Length > 0);

                foreach (var apiProgram in apiPrograms)
                {
                    var programId = DbContext.Programs.Where(p => p.ExternalId == apiProgram.Id).FirstOrDefault().Id;

                    foreach (var student in apiProgram.StudentGroupMembers)
                    {
                        if (DbContext.Students.Any(s => s.ExternalId == student.Student.Id))
                        {
                            var dbStudentId = DbContext.Students.Where(s => s.ExternalId == student.Student.Id).SingleOrDefault().Id;

                            if (!DbContext.StudentProgramRelations.Any(sp => Convert.ToInt32(sp.StudentId.ToString() + sp.ProgramId.ToString())
                            == Convert.ToInt32(dbStudentId.ToString() + programId.ToString())))
                            {
                                var apiStudent = studentResponse.Data.Students.Where(s => s.Id == student.Student.Id).SingleOrDefault();

                                var program = DbContext.Programs.Where(p => p.Id == programId).SingleOrDefault();

                                foreach (var educationPlan in apiStudent.EducationPlans)
                                {
                                    foreach (var part in educationPlan.Parts)
                                    {
                                        if (part.Code == program.Code )
                                        {
                                            var relation = new StudentProgramRelationModel()
                                            {
                                                StudentId = dbStudentId,
                                                ProgramId = programId,
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
                                HashSet<int> apiIds = new(apiProgram.StudentGroupMembers.Select(s => s.Student.Id));

                                HashSet<int> dbIds = new(DbContext.StudentProgramRelations.Where(c => c.ProgramId == programId)
                                    .Include(p => p.Student)
                                    .Select(s => s.Student.ExternalId));

                                var idDifferences = dbIds.Except(apiIds).ToList();

                                if (idDifferences.Count != 0)
                                {
                                    foreach (var externalId in idDifferences)
                                    {
                                        dbStudentId = DbContext.Students.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

                                        var relation = DbContext.StudentProgramRelations.Where(s => s.StudentId == dbStudentId)
                                            .Where(c => c.ProgramId == programId).SingleOrDefault();

                                        DbWorker.RemoveStudentProgramRelation(relation, DbContext);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}