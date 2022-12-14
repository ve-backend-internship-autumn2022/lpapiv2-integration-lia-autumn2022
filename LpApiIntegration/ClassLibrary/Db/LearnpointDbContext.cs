using LpApiIntegration.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace LpApiIntegration.FetchFromV2.Db
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
