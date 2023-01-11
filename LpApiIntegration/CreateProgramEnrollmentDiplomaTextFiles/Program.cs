using LpApiIntegration.Db;
using Microsoft.EntityFrameworkCore;

LearnpointDbContext dbContext = new();

var enrollmentWithGrades = dbContext.ProgramEnrollments
    .Include(e => e.Student)
    .Include(e => e.Program)
    .Include(pe => pe.Grades.Where(g => g.BestCourseSelectionMeritSort == 1)).ThenInclude(g => g.CourseDefinition);

foreach (var enrollment in enrollmentWithGrades.Where(e => e.Grades.Count > 0).ToList())
{
    using StreamWriter sw = new StreamWriter(@$"..\..\..\DiplomasAlternative2\{enrollment.Student.NationalRegistrationNumber} {enrollment.Student.FullName} {enrollment.Program.Code} {enrollment.Program.Name}.txt");
    {
        sw.WriteLine("----------------------------------");
        sw.WriteLine($"Diploma {enrollment.Program.Name}");
        sw.WriteLine($"{enrollment.Student.NationalRegistrationNumber} {enrollment.Student.FullName}");
        sw.WriteLine("----------------------------------");

        foreach (var grade in enrollment.Grades)
        {
            sw.WriteLine($"{grade.CourseDefinition.Name} | {grade.GradeCode}");            
        }

        sw.WriteLine("----------------------------------");
        sw.Close();
    }
}