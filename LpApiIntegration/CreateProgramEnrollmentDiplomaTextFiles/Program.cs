using LpApiIntegration.Db;
using System.Diagnostics;

LearnpointDbContext dbContext = new();

var enrollments = dbContext.ProgramEnrollments.ToList();

var grades = from programEnrollment in dbContext.ProgramEnrollments
             join program in dbContext.Programs on programEnrollment.ProgramInstanceId equals program.Id
             join student in dbContext.Students on programEnrollment.StudentId equals student.Id
             join studentGrade in dbContext.Grades on programEnrollment.Id equals studentGrade.GradedProgramEnrollmentId
             join courseDefinitions in dbContext.CourseDefinitions on studentGrade.GradedCourseDefinitionId equals courseDefinitions.Id
             select new { programEnrollment, program, studentGrade, courseDefinitions, student };

//Console.Write("Student Fullname: ");
//string? studentName = Console.ReadLine();
//if (!string.IsNullOrWhiteSpace(studentName))
//{
//    grades = from programEnrollment in dbContext.ProgramEnrollments
//             join studentGrade in dbContext.Grades on programEnrollment.Id equals studentGrade.GradedProgramEnrollmentId
//             join program in dbContext.Programs on programEnrollment.ProgramInstanceId equals program.Id
//             join student in dbContext.Students on studentGrade.GradedStudentId equals student.Id
//             join courseDefinitions in dbContext.CourseDefinitions on studentGrade.GradedCourseDefinitionId equals courseDefinitions.Id
//             where student.FullName == studentName
//             select new { programEnrollment, program, studentGrade, courseDefinitions, student };
//}

grades.ToList();

foreach (var enrollment in enrollments)
{
    var studentGrades = grades.Where(p => p.programEnrollment.Id == enrollment.Id).FirstOrDefault();

    if (studentGrades != null)
    {
        using StreamWriter sw = new StreamWriter(@$"..\..\..\Diplomas\{studentGrades.student.NationalRegistrationNumber} {studentGrades.student.FullName} {studentGrades.program.Code} {studentGrades.program.Name}.txt");
        {
            sw.WriteLine("----------------------------------");
            sw.WriteLine($"Diploma {studentGrades.program.Name}");
            sw.WriteLine($"{studentGrades.student.NationalRegistrationNumber} {studentGrades.student.FullName}");
            sw.WriteLine("----------------------------------");

            foreach (var grade in studentGrades.student.Gradings)
            {
                if (grade.GradedProgramEnrollmentId == studentGrades.programEnrollment.Id && grade.BestCourseSelectionMeritSort == 1)
                {
                    sw.WriteLine($"{grade.CourseDefinition.Name} | {grade.GradeCode}");
                }
            }

            sw.WriteLine("----------------------------------");
            sw.Close();
        }
    }
}








