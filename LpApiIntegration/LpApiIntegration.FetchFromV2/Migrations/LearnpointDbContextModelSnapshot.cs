﻿// <auto-generated />
using System;
using LpApiIntegration.FetchFromV2.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LpApiIntegration.FetchFromV2.Migrations
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

            modelBuilder.Entity("LpApiIntegration.FetchFromV2.Db.Models.CourseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExternalId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LifespanFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LifespanUntil")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LpApiIntegration.FetchFromV2.Db.Models.StaffCourseRelationModel", b =>
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

            modelBuilder.Entity("LpApiIntegration.FetchFromV2.Db.Models.StaffModel", b =>
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

            modelBuilder.Entity("LpApiIntegration.FetchFromV2.Db.Models.StudentCourseRelationModel", b =>
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

            modelBuilder.Entity("LpApiIntegration.FetchFromV2.Db.Models.StudentModel", b =>
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

            modelBuilder.Entity("LpApiIntegration.FetchFromV2.Db.Models.StaffCourseRelationModel", b =>
                {
                    b.HasOne("LpApiIntegration.FetchFromV2.Db.Models.CourseModel", "Course")
                        .WithMany("StaffMemberships")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LpApiIntegration.FetchFromV2.Db.Models.StaffModel", "StaffMember")
                        .WithMany("CourseMemberships")
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("StaffMember");
                });

            modelBuilder.Entity("LpApiIntegration.FetchFromV2.Db.Models.StudentCourseRelationModel", b =>
                {
                    b.HasOne("LpApiIntegration.FetchFromV2.Db.Models.CourseModel", "Course")
                        .WithMany("StudentMemberships")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LpApiIntegration.FetchFromV2.Db.Models.StudentModel", "Student")
                        .WithMany("CourseMemberships")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LpApiIntegration.FetchFromV2.Db.Models.CourseModel", b =>
                {
                    b.Navigation("StaffMemberships");

                    b.Navigation("StudentMemberships");
                });

            modelBuilder.Entity("LpApiIntegration.FetchFromV2.Db.Models.StaffModel", b =>
                {
                    b.Navigation("CourseMemberships");
                });

            modelBuilder.Entity("LpApiIntegration.FetchFromV2.Db.Models.StudentModel", b =>
                {
                    b.Navigation("CourseMemberships");
                });
#pragma warning restore 612, 618
        }
    }
}
