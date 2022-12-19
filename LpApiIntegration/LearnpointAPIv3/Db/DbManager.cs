
using LearnpointAPIv3;
using LearnpointAPIv3.API;
using LpApiIntegration.Db;
using LpApiIntegration.FetchFromV3.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace LpApiIntegration.FetchFromV2.Db
{
    internal class DbManager
    {
        private static LearnpointDbContext DbContext = new();

        public static void StudentManager(List<User> Students, UserApiResponse student)
        {
            var apiStudents = Students;

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

        public static void CourseManager(List<CourseDefinition> courseDefinitions, List<CourseInstance> courseInstances)
        {
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

        public static void StaffManager(List<User> staffMembers)
        {
            var apiStaffMembers = staffMembers;

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

        public static void ProgramManager(List<ProgramInstance> programs)
        {
            var apiPrograms = programs;

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

        public static void RelationshipManager(List<CourseStaffMembership> courseStaffRelations, List<CourseInstance> courseInstances, List<CourseEnrollment> courseEnrollments, List<ProgramEnrollment> programEnrollments, ApiSettings apiSettings)
        {
            StudentCourseRelations(courseInstances, courseEnrollments);
            StaffCourseRelation(courseStaffRelations, courseInstances);
            StudentProgramRelation(programEnrollments, apiSettings);

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
                    var student = DbContext.Students.Where(s => s.ExternalId == relation.UserId).SingleOrDefault();
                    var courseId = DbContext.Courses.Where(c => c.ExternalId == relation.CourseInstanceId).SingleOrDefault().Id;

                    if (DbContext.StudentCourseRelations.Any(sc => sc.StudentId == student.Id && sc.CourseId == courseId))
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
                                var studentId = DbContext.Students.Where(e => e.ExternalId == externalId).SingleOrDefault().Id;

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
                            StudentId = student.Id,
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

                foreach (var instance in courseInstances)
                {
                    relations.Add(courseStaffRelations.Where(cs => cs.CourseInstanceId == instance.Id).SingleOrDefault());
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
                var userIds = new List<int>();
                
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
                    if (!DbContext.Students.Any(s => s.ExternalId == relation.UserId))
                    {
                        userIds.Add(relation.UserId);
                    }
                }

                var lookupFilter = new { Ids = userIds.ToArray() };
                FetchFromApi.GetEnrollmentStudents(apiSettings, lookupFilter.Ids);

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
        }
    }
}