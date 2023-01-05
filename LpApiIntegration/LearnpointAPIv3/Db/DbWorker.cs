﻿using LpApiIntegration.Db;
using LpApiIntegration.Db.Db.Models;
using LpApiIntegration.FetchFromV3.API.Models;

namespace LpApiIntegration.FetchFromV2.Db
{
    internal class DbWorker
    {
        // Student
        public static void AddStudent(User apiStudent, LearnpointDbContext dbContext)
        {
            dbContext.Students.Add(
            new StudentModel()
            {
                ExternalId = apiStudent.Id,
                NationalRegistrationNumber = apiStudent.NationalRegistrationNumber,
                Username = apiStudent.Username,
                Email = apiStudent.Email,
                Email2 = apiStudent.Email2,
                MobilePhone = apiStudent.Phone,
                HomePhone = apiStudent.Phone2,
                FullName = apiStudent.FirstName + " " + apiStudent.LastName,
                IsActive = apiStudent.IsActiveStudent
            });
        }

        public static void UpdateStudent(User apiStudent, LearnpointDbContext dbContext)
        {
            var dbStudent = dbContext.Students.Where(s => s.ExternalId == apiStudent.Id).SingleOrDefault();

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
            if (dbStudent.MobilePhone != apiStudent.Phone)
            {
                dbStudent.MobilePhone = apiStudent.Phone;
            }
            if (dbStudent.HomePhone != apiStudent.Phone2)
            {
                dbStudent.HomePhone = apiStudent.Phone2;
            }
            if (dbStudent.FullName != apiStudent.FirstName + " " + apiStudent.LastName)
            {
                dbStudent.FullName = apiStudent.FirstName + " " + apiStudent.LastName;
            }
            if (dbStudent.IsActive != true)
            {
                dbStudent.IsActive = apiStudent.IsActiveStudent;
            }
        }
        public static void CheckForInactiveStudents(List<User> students, LearnpointDbContext dbContext)
        {
            HashSet<int> diffids = new(students.Select(s => s.Id));
            var results = dbContext.Students.Where(s => !diffids.Contains(s.ExternalId)).ToList();

            foreach (var student in results)
            {
                student.IsActive = false;
            }
        }

        // Course

        public static void AddCourseDefinition(CourseDefinition apiCourseDefinition, LearnpointDbContext dbContext)
        {
            dbContext.CourseDefinitions.Add(
            new CourseDefinitionModel()
            {
                ExternalId = apiCourseDefinition.Id,
                Code = apiCourseDefinition.Code,
                Name = apiCourseDefinition.Name,
                Description = apiCourseDefinition.Description,
                IsInternship = apiCourseDefinition.IsInternship,
                Points = apiCourseDefinition.Points
            });
        }

        public static void UpdateCourseDefinition(CourseDefinition apiCourseDefinition, LearnpointDbContext dbContext)
        {
            var dbCourseDefinition = dbContext.CourseDefinitions.Where(s => s.ExternalId == apiCourseDefinition.Id).SingleOrDefault();

            if (dbCourseDefinition.ExternalId != apiCourseDefinition.Id)
            {
                dbCourseDefinition.ExternalId = apiCourseDefinition.Id;
            }
            if (dbCourseDefinition.Code != apiCourseDefinition.Code)
            {
                dbCourseDefinition.Code = apiCourseDefinition.Code;
            }
            if (dbCourseDefinition.Name != apiCourseDefinition.Name)
            {
                dbCourseDefinition.Name = apiCourseDefinition.Name;
            }
            if (dbCourseDefinition.Description != apiCourseDefinition.Description)
            {
                dbCourseDefinition.Description = apiCourseDefinition.Description;
            }
            if (dbCourseDefinition.IsInternship != apiCourseDefinition.IsInternship)
            {
                dbCourseDefinition.IsInternship = apiCourseDefinition.IsInternship;
            }
            if (dbCourseDefinition.Points != apiCourseDefinition.Points)
            {
                dbCourseDefinition.Points = apiCourseDefinition.Points;
            }
        }

        public static void AddCourse(CourseDefinition courseDefinition, CourseInstance courseInstance, LearnpointDbContext dbContext)
        {
            dbContext.Courses.Add(
                   new CourseModel()
                   {
                       ExternalId = courseInstance.Id,
                       Name = courseDefinition.Name,
                       Code = courseDefinition.Code,
                       LifespanFrom = courseInstance.From,
                       LifespanUntil = courseInstance.To,
                       Points = courseDefinition.Points,
                   });
        }

        public static void UpdateCourse(CourseDefinition courseDefinition, CourseInstance courseInstance, LearnpointDbContext dbContext)
        {
            var dbCourse = dbContext.Courses.Where(c => c.ExternalId == courseInstance.Id).SingleOrDefault();

            if (dbCourse.Name != courseDefinition.Name)
            {
                dbCourse.Name = courseDefinition.Name;
            }
            if (dbCourse.Code != courseDefinition.Code)
            {
                dbCourse.Code = courseDefinition.Code;
            }
            if (dbCourse.LifespanFrom != courseInstance.From)
            {
                dbCourse.LifespanFrom = courseInstance.From;
            }
            if (dbCourse.LifespanUntil != courseInstance.To)
            {
                dbCourse.LifespanUntil = courseInstance.To;
            }
            if (dbCourse.Points != courseDefinition.Points)
            {
                dbCourse.Points = courseDefinition.Points;
            }
        }

        // Staff

        public static void AddStaffMember(User apiStaffMember, LearnpointDbContext dbContext)
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
                MobilePhone = apiStaffMember.Phone,
                Phone2 = apiStaffMember.Phone2
            });
        }

        public static void UpdateStaffMember(User apiStaffMember, LearnpointDbContext dbContext)
        {
            var dbStaff = dbContext.StaffMembers.Where(s => s.ExternalId == apiStaffMember.Id).SingleOrDefault();

            if (dbStaff.NationalRegistrationNumber != apiStaffMember.NationalRegistrationNumber)
            {
                dbStaff.NationalRegistrationNumber = apiStaffMember.NationalRegistrationNumber;
            }
            if (dbStaff.Signature != apiStaffMember.Signature)
            {
                dbStaff.Signature = apiStaffMember.Signature;
            }
            if (dbStaff.FullName != apiStaffMember.FirstName + " " + apiStaffMember.LastName)
            {
                dbStaff.FullName = apiStaffMember.FirstName + " " + apiStaffMember.LastName;
            }
            if (dbStaff.Username != apiStaffMember.Username)
            {
                dbStaff.Username = apiStaffMember.Username;
            }
            if (dbStaff.Email != apiStaffMember.Email)
            {
                dbStaff.Email = apiStaffMember.Email;
            }
            if (dbStaff.Email2 != apiStaffMember.Email2)
            {
                dbStaff.Email2 = apiStaffMember.Email2;
            }
            if (dbStaff.MobilePhone != apiStaffMember.Phone)
            {
                dbStaff.MobilePhone = apiStaffMember.Phone;
            }
            if (dbStaff.Phone2 != apiStaffMember.Phone2)
            {
                dbStaff.Phone2 = apiStaffMember.Phone2;
            }
        }

        // Program

        public static void AddProgram(ProgramInstance apiProgram, LearnpointDbContext dbContext)
        {
            dbContext.Programs.Add(
                new ProgramModel()
                {
                    ExternalId = apiProgram.Id,
                    Code = apiProgram.Code,
                    Name = apiProgram.Name,
                    LifespanFrom = apiProgram.From,
                    LifespanUntil = apiProgram.To
                });
        }
        public static void UpdateProgram(ProgramInstance apiProgram, LearnpointDbContext dbContext)
        {
            var dbProgram = dbContext.Programs.Where(p => p.ExternalId == apiProgram.Id).SingleOrDefault();

            if (dbProgram.Code != apiProgram.Code)
            {
                dbProgram.Code = apiProgram.Code;
            }
            if (dbProgram.Name != apiProgram.Name)
            {
                dbProgram.Name = apiProgram.Name;
            }
            if (dbProgram.LifespanFrom != apiProgram.From)
            {
                dbProgram.LifespanFrom = apiProgram.From;
            }
            if (dbProgram.LifespanUntil != apiProgram.To)
            {
                dbProgram.LifespanUntil = apiProgram.To;
            }
        }

        public static void AddProgramEnrollment(ProgramEnrollment programEnrollment, LearnpointDbContext dbContext)
        {

            dbContext.ProgramEnrollments.Add(
                new ProgramEnrollmentModel()
                {
                    ExternalId = programEnrollment.Id,
                    StudentId = dbContext.Students.Where(s => s.ExternalId == programEnrollment.UserId).SingleOrDefault().Id,
                    ProgramInstanceId = dbContext.Programs.Where(p => p.ExternalId == programEnrollment.ProgramInstanceId).SingleOrDefault().Id,
                    Enrolled = programEnrollment.Enrolled,
                    Unenrolled = programEnrollment.Unenrolled,
                    Active = programEnrollment.Active,
                    Canceled = programEnrollment.Canceled,
                    Changed = programEnrollment.Changed,
                    DiplomaDate = programEnrollment.DiplomaDate
                });
        }

        public static void UpdateProgramEnrollment(ProgramEnrollment programEnrollment, LearnpointDbContext dbContext)
        {
            var dbProgramEnrollment = dbContext.ProgramEnrollments.Where(p => p.ExternalId == programEnrollment.Id).SingleOrDefault();
            var dbStudentId = dbContext.Students.Where(s => s.ExternalId == programEnrollment.UserId).SingleOrDefault().Id;
            var dbProgramId = dbContext.Programs.Where(p => p.ExternalId == programEnrollment.ProgramInstanceId).SingleOrDefault().Id;

            if (dbProgramEnrollment.StudentId != dbStudentId)
            {
                dbProgramEnrollment.StudentId = dbStudentId;
            }
            if (dbProgramEnrollment.ProgramInstanceId != dbProgramId)
            {
                dbProgramEnrollment.ProgramInstanceId = dbProgramId;
            }
            if (dbProgramEnrollment.Enrolled != programEnrollment.Enrolled)
            {
                dbProgramEnrollment.Enrolled = programEnrollment.Enrolled;
            }
            if (dbProgramEnrollment.Unenrolled != programEnrollment.Unenrolled)
            {
                dbProgramEnrollment.Unenrolled = programEnrollment.Unenrolled;
            }
            if (dbProgramEnrollment.Active != programEnrollment.Active)
            {
                dbProgramEnrollment.Active = programEnrollment.Active;
            }
            if (dbProgramEnrollment.Canceled != programEnrollment.Canceled)
            {
                dbProgramEnrollment.Canceled = programEnrollment.Canceled;
            }
            if (dbProgramEnrollment.Changed != programEnrollment.Changed)
            {
                dbProgramEnrollment.Changed = programEnrollment.Changed;
            }
            if (dbProgramEnrollment.DiplomaDate != programEnrollment.DiplomaDate)
            {
                dbProgramEnrollment.DiplomaDate = programEnrollment.DiplomaDate;
            }
        }

        //CourseGrade

        public static void UpdateCourseGrade(CourseGrade grade, int dbStudentId, int dbStaffId, int? dbCourseDefinitionId, int? dbCourseId, int? dbProgramEnrollmentId, LearnpointDbContext dbContext)
        {
            var dbCourseGrade = dbContext.Grades.Where(p => p.ExternalId == grade.Id).SingleOrDefault();

            if (dbCourseGrade.GradedStudentId != dbStudentId)
            {
                dbCourseGrade.GradedStudentId = dbStudentId;
            }
            if (dbCourseGrade.GradingStaffId != dbStaffId)
            {
                dbCourseGrade.GradingStaffId = dbStaffId;
            }
            if (dbCourseGrade.GradedCourseDefinitionId != dbCourseDefinitionId)
            {
                dbCourseGrade.GradedCourseDefinitionId = dbCourseDefinitionId;
            }
            if (dbCourseGrade.GradedProgramEnrollmentId != dbProgramEnrollmentId)
            {
                dbCourseGrade.GradedProgramEnrollmentId = dbProgramEnrollmentId;
            }
            if (dbCourseGrade.Grade != grade.Name)
            {
                dbCourseGrade.Grade = grade.Name;
            }
            if (dbCourseGrade.GradeCode != grade.Code)
            {
                dbCourseGrade.GradeCode = grade.Code;
            }
            if (dbCourseGrade.GradePoints != grade.Value)
            {
                dbCourseGrade.GradePoints = grade.Value;
            }
            if (dbCourseGrade.OfficialGradingDate != grade.OfficialGradingDate)
            {
                dbCourseGrade.OfficialGradingDate = grade.OfficialGradingDate;
            }
            if (dbCourseGrade.Published != grade.Published)
            {
                dbCourseGrade.Published = grade.Published;
            }
            if (dbCourseGrade.GradedCourseInstanceId != dbCourseId)
            {
                dbCourseGrade.GradedCourseInstanceId = dbCourseId;
            }
            if (dbCourseGrade.BestCourseSelectionMeritSort != grade.BestCourseSelectionMeritSort)
            {
                dbCourseGrade.BestCourseSelectionMeritSort = grade.BestCourseSelectionMeritSort;
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
        public static void AddStudentGradeRelation(GradingModel relation, LearnpointDbContext dbContext)
        {
            dbContext.Grades.Add(relation);
        }

        public static void RemoveCourseStudentRelation(StudentCourseRelationModel relation, LearnpointDbContext dbContext)
        {
            dbContext.StudentCourseRelations.Remove(relation);
        }
        public static void RemoveCourseStaffRelation(StaffCourseRelationModel relation, LearnpointDbContext dbContext)
        {
            dbContext.StaffCourseRelations.Remove(relation);
        }
        public static void RemoveStudentProgramRelation(StudentProgramRelationModel relation, LearnpointDbContext dbContext)
        {
            dbContext.StudentProgramRelations.Remove(relation);
        }
        public static void RemoveStudentGrade(GradingModel relation, LearnpointDbContext dbContext)
        {
            dbContext.Grades.Remove(relation);
        }
    }
}




