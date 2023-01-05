using LpApiIntegration.Db;
using LpApiIntegration.Db.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace LpApiIntegration.Db
{
    public class LearnpointDbContext : DbContext
    {
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<StudentCourseRelationModel> StudentCourseRelations { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<StaffCourseRelationModel> StaffCourseRelations { get; set; }
        public DbSet<StaffModel> StaffMembers { get; set; }
        public DbSet<ProgramModel> Programs { get; set; }
        public DbSet<StudentProgramRelationModel> StudentProgramRelations { get; set; }
        public DbSet<GradingModel> Grades { get; set; }
        public DbSet<ProgramEnrollmentModel> ProgramEnrollments { get; set; }
        //public DbSet<CourseGradeModel> CourseGrades { get; set; }
        public DbSet<CourseDefinitionModel> CourseDefinitions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(
                Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
            IConfiguration _configuration = builder.Build();
            var connectionString = _configuration.GetConnectionString("DevConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
