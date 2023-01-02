﻿// <auto-generated />
using System;
using LpApiIntegration.FetchFromV2.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LpApiIntegration.Db.Migrations
{
    [DbContext(typeof(LearnpointDbContext))]
    partial class LearnpointDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LpApiIntegration.Db.CourseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LifespanFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LifespanUntil")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LpApiIntegration.Db.Db.Models.CourseDefinitionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<bool>("IsInternship")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CourseDefinitions");
                });

            modelBuilder.Entity("LpApiIntegration.Db.Db.Models.GradingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GradeCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("GradePoints")
                        .HasColumnType("float");

                    b.Property<int?>("GradedCourseDefinitionId")
                        .HasColumnType("int");

                    b.Property<int?>("GradedCourseInstanceId")
                        .HasColumnType("int");

                    b.Property<int?>("GradedProgramEnrollmentId")
                        .HasColumnType("int");

                    b.Property<int>("GradedStudentId")
                        .HasColumnType("int");

                    b.Property<int>("GradingStaffId")
                        .HasColumnType("int");

                    b.Property<string>("OfficialGradingDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Published")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GradedCourseDefinitionId");

                    b.HasIndex("GradedCourseInstanceId");

                    b.HasIndex("GradedStudentId");

                    b.HasIndex("GradingStaffId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("LpApiIntegration.Db.Db.Models.ProgramEnrollmentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<bool>("Canceled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime2");

                    b.Property<string>("DiplomaDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Enrolled")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<int>("ProgramInstanceId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Unenrolled")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProgramInstanceId");

                    b.HasIndex("StudentId");

                    b.ToTable("ProgramEnrollments");
                });

            modelBuilder.Entity("LpApiIntegration.Db.ProgramModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LifespanFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LifespanUntil")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("LpApiIntegration.Db.StaffCourseRelationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("StaffMemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("StaffCourseRelations");
                });

            modelBuilder.Entity("LpApiIntegration.Db.StaffModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<int>("ExternalUserId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MayExposeMobilePhoneToStudents")
                        .HasColumnType("bit");

                    b.Property<bool>("MayExposePhone2ToStudents")
                        .HasColumnType("bit");

                    b.Property<string>("MobilePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalRegistrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Signature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StaffMembers");
                });

            modelBuilder.Entity("LpApiIntegration.Db.StudentCourseRelationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourseRelations");
                });

            modelBuilder.Entity("LpApiIntegration.Db.StudentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<int>("ExternalUserId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MobilePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalRegistrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LpApiIntegration.Db.StudentProgramRelationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActiveStudent")
                        .HasColumnType("bit");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.Property<string>("StateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentProgramRelations");
                });

            modelBuilder.Entity("LpApiIntegration.Db.Db.Models.GradingModel", b =>
                {
                    b.HasOne("LpApiIntegration.Db.Db.Models.CourseDefinitionModel", "CourseDefinition")
                        .WithMany("Gradings")
                        .HasForeignKey("GradedCourseDefinitionId");

                    b.HasOne("LpApiIntegration.Db.CourseModel", "Course")
                        .WithMany("Gradings")
                        .HasForeignKey("GradedCourseInstanceId");

                    b.HasOne("LpApiIntegration.Db.StudentModel", "Student")
                        .WithMany("Gradings")
                        .HasForeignKey("GradedStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LpApiIntegration.Db.StaffModel", "StaffMember")
                        .WithMany("StudentGrade")
                        .HasForeignKey("GradingStaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("CourseDefinition");

                    b.Navigation("StaffMember");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LpApiIntegration.Db.Db.Models.ProgramEnrollmentModel", b =>
                {
                    b.HasOne("LpApiIntegration.Db.ProgramModel", "Program")
                        .WithMany("ProgramEnrollments")
                        .HasForeignKey("ProgramInstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LpApiIntegration.Db.StudentModel", "Student")
                        .WithMany("ProgramEnrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Program");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LpApiIntegration.Db.StaffCourseRelationModel", b =>
                {
                    b.HasOne("LpApiIntegration.Db.CourseModel", "Course")
                        .WithMany("StaffMemberships")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LpApiIntegration.Db.StaffModel", "StaffMember")
                        .WithMany("CourseMemberships")
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("StaffMember");
                });

            modelBuilder.Entity("LpApiIntegration.Db.StudentCourseRelationModel", b =>
                {
                    b.HasOne("LpApiIntegration.Db.CourseModel", "Course")
                        .WithMany("StudentMemberships")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LpApiIntegration.Db.StudentModel", "Student")
                        .WithMany("CourseMemberships")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LpApiIntegration.Db.StudentProgramRelationModel", b =>
                {
                    b.HasOne("LpApiIntegration.Db.ProgramModel", "Program")
                        .WithMany()
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LpApiIntegration.Db.StudentModel", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Program");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LpApiIntegration.Db.CourseModel", b =>
                {
                    b.Navigation("Gradings");

                    b.Navigation("StaffMemberships");

                    b.Navigation("StudentMemberships");
                });

            modelBuilder.Entity("LpApiIntegration.Db.Db.Models.CourseDefinitionModel", b =>
                {
                    b.Navigation("Gradings");
                });

            modelBuilder.Entity("LpApiIntegration.Db.ProgramModel", b =>
                {
                    b.Navigation("ProgramEnrollments");
                });

            modelBuilder.Entity("LpApiIntegration.Db.StaffModel", b =>
                {
                    b.Navigation("CourseMemberships");

                    b.Navigation("StudentGrade");
                });

            modelBuilder.Entity("LpApiIntegration.Db.StudentModel", b =>
                {
                    b.Navigation("CourseMemberships");

                    b.Navigation("Gradings");

                    b.Navigation("ProgramEnrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
