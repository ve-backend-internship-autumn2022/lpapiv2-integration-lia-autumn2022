using CreateProgramEnrollmentDiplomaTextFiles;
using LpApiIntegration.Db;
using LpApiIntegration.Db.Db.Models;
using LpApiIntegration.FetchFromV2.Db;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

LearnpointDbContext dbContext = new();

var grades = from enrollment in dbContext.ProgramEnrollments
             join program in dbContext.Programs on enrollment.ProgramInstanceId equals program.Id
             join studentGrade in dbContext.Grades on enrollment.ProgramInstanceId equals studentGrade.GradedProgramEnrollmentId
             join courseInstances in dbContext.Courses on studentGrade.GradedCourseInstanceId equals courseInstances.Id
             join courseDefinitions in dbContext.CourseDefinitions on studentGrade.GradedCourseDefinitionId equals courseDefinitions.Id
             join student in dbContext.Students on studentGrade.GradedStudentId equals student.Id
             select new { /*enrollment.Id, program.ExternalId, */studentGrade/*, courseInstances, courseDefinitions, studentId = student.Id*/ };

var enrollments = from enrollment in dbContext.ProgramEnrollments
                  join program in dbContext.Programs on enrollment.ProgramInstanceId equals program.Id
                  select new { enrollment, program };

//var enrollments = dbContext.ProgramEnrollments.ToList();
//var grades = dbContext.Grades.ToList();



foreach (var enrollment in dbContext.ProgramEnrollments.ToList()) 
{
    //var grades = from programEnrollment in dbContext.ProgramEnrollments
    //             join program in dbContext.Programs on enrollment.ProgramInstanceId equals program.Id
    //             join studentGrade in dbContext.Grades on enrollment.ProgramInstanceId equals studentGrade.GradedProgramEnrollmentId
    //             join courseInstances in dbContext.Courses on studentGrade.GradedCourseInstanceId equals courseInstances.Id
    //             join courseDefinitions in dbContext.CourseDefinitions on studentGrade.GradedCourseDefinitionId equals courseDefinitions.Id
    //             join student in dbContext.Students on studentGrade.GradedStudentId equals student.Id
    //             //where programEnrollment.Id == enrollment.Id
    //             select new { enrollment, program, studentGrade, courseInstances, courseDefinitions, student };

    //Console.WriteLine($"Diploma {grades.program.Name}");
    //Console.WriteLine("----------------------------------");
    //Console.WriteLine($"{grades.student.NationalRegistrationNumber} {grades.student.FullName}\n");
    //Console.WriteLine("----------------------------------\n");
    //Console.WriteLine($"{grades.courseDefinitions.Name} | {grades.studentGrade.GradeCode}");


    foreach (var grade in grades)
    {
        //using StreamWriter sw = new StreamWriter($"{grade.student.NationalRegistrationNumber} {grade.student.FullName} {grade.program.Code} {grade.program.Name}.txt");
        //{
        //    sw.WriteLine($"Diploma {grade.program.Name}");
        //    sw.WriteLine("----------------------------------");
        //    sw.WriteLine($"{grade.student.NationalRegistrationNumber} {grade.student.FullName}\n");
        //    sw.WriteLine("----------------------------------\n");


        //    sw.WriteLine($"{grade.courseDefinitions.Name} | {grade.studentGrade.GradeCode}");

        //    sw.WriteLine();

        //}

       

    }

    

    //CourseDefinitionModel dbCourseDefinition = null;
    //CourseModel dbCourse = null;

    //if (dbContext.Grades.Any(c => c.GradedProgramEnrollmentId == enrollment.Id))
    //{
    //    var student = dbContext.Students.Where(s => s.ExternalId == enrollment.StudentId).SingleOrDefault();

    //    int? staffId = null;

    //    foreach (var item in grades)
    //    {

    //        //var grade = dbContext.Grades.Where(c => c.GradedProgramEnrollmentId == enrollment.Id && c.GradingStaffId == item.GradingStaffId).SingleOrDefault();

    //    }



    //    //var grade = dbContext.Grades.Where(c => c.GradedProgramEnrollmentId == enrollment.Id && c.GradingStaffId == staffId).SingleOrDefault();

    //    var program = dbContext.Programs.Where(p => p.ExternalId == enrollment.ProgramInstanceId).SingleOrDefault();

    //    //if (grade.GradedCourseInstanceId == null)
    //    //{
    //    //    dbCourseDefinition = dbContext.CourseDefinitions.Where(c => c.ExternalId == grade.GradedCourseDefinitionId).SingleOrDefault();
    //    //}
    //    //else
    //    //{
    //    //    dbCourse = dbContext.Courses.Where(c => c.ExternalId == grade.GradedCourseInstanceId).SingleOrDefault();
    //    //}

    //    using StreamWriter sw = new StreamWriter($"{student.NationalRegistrationNumber} {student.FullName} {program.Code} {program.Name}.txt");
    //    {
    //        sw.WriteLine("----------------------------------");
    //        sw.WriteLine($"Diploma {program.Name}");
    //        sw.WriteLine($"{student.NationalRegistrationNumber} {student.FullName}\n");
    //        sw.WriteLine("----------------------------------\n");


    //        //sw.WriteLine($"{dbCourseDefinition.Name} | {grade.Grade}");

    //        sw.WriteLine();

    //    }

    //    Console.WriteLine($"{student.NationalRegistrationNumber} {student.FullName} {program.Code} {program.Name}.txt");
    //}   







}

Console.ReadLine();


