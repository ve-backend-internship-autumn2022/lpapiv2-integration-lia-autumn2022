using LearnpointAPIv3;
using LpApiIntegration.Db;
using LpApiIntegration.Db.Db.Models;
using LpApiIntegration.FetchFromV3.API.Models;
using LpApiIntegration.FetchFromV3.Functions;
using Microsoft.EntityFrameworkCore;

namespace LpApiIntegration.FetchFromV2.Db
{
    internal class DbManager
    {
        private static readonly LearnpointDbContext DbContext = new();

        public static void StudentManager(List<User> students, List<ProgramEnrollment> programEnrollments, List<ProgramInstance> programs, List<CourseGrade> courseGrades, ApiSettings apiSettings)
        {
            foreach (var student in FetchData.GetEnrollmentStudents(programEnrollments, programs, apiSettings, DbContext))
            {
                if (!students.Any(s => s.Id == student.Id))
                {
                    students.Add(student);
                }
            }
            
            foreach (var student in FetchData.GetGradingStudents(courseGrades, apiSettings, DbContext))
            {
                if (!students.Any(s => s.Id == student.Id))
                {
                    students.Add(student);
                }
            }

            var apiStudents = students;

            foreach (var apiStudent in apiStudents)
            {
                if (DbContext.Students.Any(s => s.ExternalId == apiStudent.Id))
                {
                    DbWorker.UpdateStudent(apiStudent, DbContext);
                }
                else
                {
                    DbWorker.AddStudent(apiStudent, DbContext);
                }
            }

            DbWorker.CheckForInactiveStudents(apiStudents, DbContext);

            DbContext.SaveChanges();
        }

        public static void CourseManager(List<CourseDefinition> courseDefinitions, List<CourseInstance> courseInstances, List<CourseGrade> courseGrades, ApiSettings apiSettings)
        {
            ManageCourseDefinitions(courseDefinitions);
            ManageCourses(courseDefinitions, courseGrades, courseInstances, apiSettings);

            static void ManageCourseDefinitions(List<CourseDefinition> courseDefinitions)
            {
                var apiCourseDefinitions = courseDefinitions;

                foreach (var apiCourseDefinition in apiCourseDefinitions)
                {
                    if (DbContext.CourseDefinitions.Any(s => s.ExternalId == apiCourseDefinition.Id))
                    {
                        DbWorker.UpdateCourseDefinition(apiCourseDefinition, DbContext);
                    }
                    else
                    {
                        DbWorker.AddCourseDefinition(apiCourseDefinition, DbContext);
                    }
                }

                DbContext.SaveChanges();
            }

            static void ManageCourses(List<CourseDefinition> courseDefinitions, List<CourseGrade> courseGrades, List<CourseInstance> courseInstances, ApiSettings apiSettings)
            {
                foreach (var courseInstance in FetchData.GetCourses(courseGrades, apiSettings, DbContext))
                {
                    if (!courseInstances.Any(c => c.Id == courseInstance.Id))
                    {
                        courseInstances.Add(courseInstance);
                    }
                }

                var apiCourses = courseDefinitions;
                var apiCourseInstances = courseInstances;

                foreach (var apiCourseInstance in apiCourseInstances)
                {
                    var apiCourse = courseDefinitions.Where(c => c.Id == apiCourseInstance.CourseDefinitionId).SingleOrDefault();

                    if (DbContext.Courses.Any(c => c.ExternalId == apiCourseInstance.Id))
                    {
                        DbWorker.UpdateCourse(apiCourse, apiCourseInstance, DbContext);
                    }
                    else
                    {
                        DbWorker.AddCourse(apiCourse, apiCourseInstance, DbContext);
                    }
                }

                DbContext.SaveChanges();
            }
        }

        public static void StaffManager(List<User> staffMembers)
        {
            var apiStaffMembers = staffMembers;

            foreach (var apiStaffMember in apiStaffMembers)
            {
                if (DbContext.StaffMembers.Any(s => s.ExternalId == apiStaffMember.Id))
                {
                    DbWorker.UpdateStaffMember(apiStaffMember, DbContext);
                }
                else
                {
                    DbWorker.AddStaffMember(apiStaffMember, DbContext);
                }
            }

            DbContext.SaveChanges();
        }

        public static void ProgramManager(List<ProgramInstance> programs, List<ProgramEnrollment> programEnrollments)
        {
            ManageProgramInstances(programs);
            ManageProgramEnrollments(programEnrollments);

            static void ManageProgramInstances(List<ProgramInstance> programs)
            {
                foreach (var programInstance in programs)
                {
                    if (DbContext.Programs.Any(c => c.ExternalId == programInstance.Id))
                    {
                        DbWorker.UpdateProgram(programInstance, DbContext);
                    }
                    else
                    {
                        DbWorker.AddProgram(programInstance, DbContext);
                    }
                }

                DbContext.SaveChanges();
            }

            static void ManageProgramEnrollments(List<ProgramEnrollment> programEnrollments)
            {
                foreach (var programEnrollment in programEnrollments)
                {
                    if (DbContext.ProgramEnrollments.Any(p => p.ExternalId == programEnrollment.Id))
                    {
                        DbWorker.UpdateProgramEnrollment(programEnrollment, DbContext);
                    }
                    else
                    {
                        DbWorker.AddProgramEnrollment(programEnrollment, DbContext);
                    }
                }

                DbContext.SaveChanges();
            }
        }

        public static void RelationshipManager(List<CourseStaffMembership> courseStaffRelations, List<CourseInstance> courseInstances, List<CourseEnrollment> courseEnrollments, List<ProgramEnrollment> programEnrollments, List<CourseGrade> courseGrades, ApiSettings apiSettings)
        {
            StudentCourseRelations(courseInstances, courseEnrollments);
            StaffCourseRelation(courseStaffRelations, courseInstances);
            StudentProgramRelation(programEnrollments, apiSettings);
            StudentGrade(courseGrades, courseInstances, apiSettings);

            DbContext.SaveChanges();

            static void StudentCourseRelations(List<CourseInstance> courseInstances, List<CourseEnrollment> courseEnrollments)
            {
                //Add and delete Student-course-relations                
                var relations = new List<CourseEnrollment>();

                foreach (var instance in courseInstances)
                {
                    var dbCourse = DbContext.Courses.Where(c => c.ExternalId == instance.Id).SingleOrDefault();

                    foreach (var enrollment in courseEnrollments)
                    {
                        if (enrollment.CourseInstanceId == dbCourse.ExternalId)
                        {
                            relations.Add(enrollment);
                        }
                    }
                }

                foreach (var relation in relations)
                {
                    var studentId = DbContext.Students.Where(s => s.ExternalId == relation.UserId).SingleOrDefault().Id;
                    var courseId = DbContext.Courses.Where(c => c.ExternalId == relation.CourseInstanceId).SingleOrDefault().Id;

                    if (DbContext.StudentCourseRelations.Any(sc => sc.StudentId == studentId && sc.CourseId == courseId))
                    {
                        HashSet<int> apiIds = new(relations.Select(s => s.UserId));

                        HashSet<int> dbIds = new(DbContext.StudentCourseRelations.Where(c => c.CourseId == courseId)
                                        .Include(p => p.Student)
                                        .Select(s => s.Student.ExternalId));

                        var idDifferences = dbIds.Except(apiIds).ToList();

                        if (idDifferences.Count != 0)
                        {
                            foreach (var externalId in idDifferences)
                            {
                                studentId = DbContext.Students.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

                                var dbRelation = DbContext.StudentCourseRelations.Where(s => s.StudentId == studentId)
                                    .Where(c => c.CourseId == courseId).SingleOrDefault();

                                DbWorker.RemoveCourseStudentRelation(dbRelation, DbContext);
                            }
                        }
                    }
                    else
                    {
                        var studentCourse = new StudentCourseRelationModel()
                        {
                            StudentId = studentId,
                            CourseId = courseId
                        };

                        DbWorker.AddCourseStudentRelation(studentCourse, DbContext);
                    }
                }
            }

            static void StaffCourseRelation(List<CourseStaffMembership> courseStaffRelations, List<CourseInstance> courseInstances)
            {
                //Add and delete staff-course-relations
                var relations = new List<CourseStaffMembership>();

                foreach (var relation in courseStaffRelations)
                {
                    var dbCourse = DbContext.Courses.Where(c => c.ExternalId == relation.CourseInstanceId).SingleOrDefault();

                    if (dbCourse != null)
                    {
                        relations.Add(relation);
                    }

                }

                foreach (var relation in relations)
                {
                    var staffId = DbContext.StaffMembers.Where(s => s.ExternalId == relation.UserId).SingleOrDefault().Id;
                    var courseId = DbContext.Courses.Where(c => c.ExternalId == relation.CourseInstanceId).SingleOrDefault().Id;

                    if (DbContext.StaffCourseRelations.Any(sc => sc.StaffMemberId == staffId && sc.CourseId == courseId))
                    {
                        HashSet<int> apiIds = new(relations.Select(s => s.UserId));

                        HashSet<int> dbIds = new(DbContext.StaffCourseRelations.Where(c => c.CourseId == courseId)
                                .Include(p => p.StaffMember)
                                .Select(s => s.StaffMember.ExternalId));

                        var idDifferences = dbIds.Except(apiIds).ToList();

                        if (idDifferences.Count != 0)
                        {
                            foreach (var externalId in idDifferences)
                            {
                                staffId = DbContext.StaffMembers.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

                                var dbRelation = DbContext.StaffCourseRelations.Where(s => s.StaffMemberId == staffId)
                                    .Where(c => c.CourseId == courseId).SingleOrDefault();

                                DbWorker.RemoveCourseStaffRelation(dbRelation, DbContext);
                            }
                        }
                    }
                    else
                    {
                        var staffCourse = new StaffCourseRelationModel()
                        {
                            StaffMemberId = staffId,
                            CourseId = courseId
                        };

                        DbWorker.AddCourseStaffRelation(staffCourse, DbContext);
                    }
                }
            }

            static void StudentProgramRelation(List<ProgramEnrollment> programEnrollments, ApiSettings apiSettings)
            {
                //Add and delete student-program-relations
                var relations = new List<ProgramEnrollment>();
                var stateName = "";

                foreach (var enrollment in programEnrollments)
                {
                    var dbProgram = DbContext.Programs.Where(p => p.ExternalId == enrollment.ProgramInstanceId).SingleOrDefault();

                    if (enrollment.ProgramInstanceId == dbProgram.ExternalId)
                    {
                        relations.Add(enrollment);
                    }
                }

                foreach (var relation in relations)
                {
                    if (DbContext.Students.Any(s => s.ExternalId == relation.UserId))
                    {
                        var studentId = DbContext.Students.Where(s => s.ExternalId == relation.UserId).SingleOrDefault().Id;
                        var programId = DbContext.Programs.Where(p => p.ExternalId == relation.ProgramInstanceId).SingleOrDefault().Id;

                        if (DbContext.StudentProgramRelations.Any(sp => sp.StudentId == studentId && sp.ProgramId == programId))
                        {
                            HashSet<int> apiIds = new(relations.Select(s => s.UserId));

                            HashSet<int> dbIds = new(DbContext.StudentProgramRelations.Where(c => c.ProgramId == programId)
                                            .Include(p => p.Student)
                                            .Select(s => s.Student.ExternalId));

                            var idDifferences = dbIds.Except(apiIds).ToList();

                            if (idDifferences.Count != 0)
                            {
                                foreach (var externalId in idDifferences)
                                {
                                    studentId = DbContext.Students.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

                                    var dbRelation = DbContext.StudentProgramRelations.Where(s => s.StudentId == studentId)
                                        .Where(c => c.ProgramId == programId).SingleOrDefault();

                                    DbWorker.RemoveStudentProgramRelation(dbRelation, DbContext);
                                }
                            }
                        }
                        else
                        {
                            if (relation.DiplomaDate != null)
                            {
                                stateName = "Slutat med examensbevis";
                            }
                            else if (relation.Active == true)
                            {
                                stateName = "Inskriven";
                            }
                            else
                            {
                                stateName = "Avregistrerad";
                            }

                            var studentProgram = new StudentProgramRelationModel()
                            {
                                StudentId = studentId,
                                ProgramId = programId,
                                IsActiveStudent = relation.Active,
                                StateName = stateName,
                                FromDate = (DateTime)relation.Changed
                            };

                            DbWorker.AddStudentProgramRelation(studentProgram, DbContext);
                        }
                    }
                }
            }

            static void StudentGrade(List<CourseGrade> courseGrade, List<CourseInstance> courseInstances, ApiSettings apiSettings)
            {
                int? dbCourseDefinitionId = null;
                int? dbCourseId = null;
                int? dbProgramEnrollmentId = null;

                foreach (var grade in courseGrade)
                {
                    var dbStudentId = DbContext.Students.Where(s => s.ExternalId == grade.UserId).SingleOrDefault().Id;

                    var dbStaffId = DbContext.StaffMembers.Where(s => s.ExternalId == grade.AwardedByUserId).SingleOrDefault().Id;

                    if (grade.AwardedInCourseInstanceId != null)
                    {
                        dbCourseId = DbContext.Courses.Where(c => c.ExternalId == grade.AwardedInCourseInstanceId).SingleOrDefault().Id;
                        dbCourseDefinitionId = DbContext.CourseDefinitions.Where(c => c.ExternalId == grade.CourseDefinitionId).SingleOrDefault().Id;
                    }
                    else
                    {
                        dbCourseDefinitionId = DbContext.CourseDefinitions.Where(c => c.ExternalId == grade.CourseDefinitionId).SingleOrDefault().Id;
                        dbCourseId = null;
                    }

                    if (DbContext.ProgramEnrollments.Any(p => p.ExternalId == grade.ProgramEnrollmentId))
                    {
                        dbProgramEnrollmentId = DbContext.ProgramEnrollments.Where(p => p.ExternalId == grade.ProgramEnrollmentId).SingleOrDefault().Id;
                    }


                    if (DbContext.Grades.Any(g => g.ExternalId == grade.Id))
                    {
                        DbWorker.UpdateCourseGrade(grade, dbStudentId, dbStaffId, dbCourseDefinitionId, dbCourseId, dbProgramEnrollmentId, DbContext);
                    }
                    else
                    {
                        var studentGrade = new GradingModel()
                        {
                            ExternalId = grade.Id,
                            GradedStudentId = dbStudentId,
                            GradingStaffId = dbStaffId,
                            GradedCourseDefinitionId = dbCourseDefinitionId,
                            GradedProgramEnrollmentId = dbProgramEnrollmentId,
                            Grade = grade.Name,
                            GradeCode = grade.Code,
                            GradePoints = grade.Value,
                            OfficialGradingDate = grade.OfficialGradingDate,
                            Published = grade.Published,
                            GradedCourseInstanceId = dbCourseId,
                            BestCourseSelectionMeritSort = grade.BestCourseSelectionMeritSort

                        };

                        DbWorker.AddStudentGradeRelation(studentGrade, DbContext);
                    }
                }

                HashSet<int> apiIds = new(courseGrade.Select(c => c.Id));

                HashSet<int> dbIds = new(DbContext.Grades.Select(g => g.ExternalId));

                var idDifferences = dbIds.Except(apiIds).ToList();

                if (idDifferences.Count != 0)
                {
                    foreach (var externalId in idDifferences)
                    {
                        var relation = DbContext.Grades.Where(g => g.ExternalId == externalId).SingleOrDefault();

                        DbWorker.RemoveStudentGrade(relation, DbContext);
                    }
                }
            }
        }
    }
}