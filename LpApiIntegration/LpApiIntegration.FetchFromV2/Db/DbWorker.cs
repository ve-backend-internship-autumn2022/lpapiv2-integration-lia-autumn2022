﻿using LpApiIntegration.FetchFromV2.StudentModels;
using LpApiIntegration.FetchFromV2.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LpApiIntegration.FetchFromV2.GroupModel;
using LpApiIntegration.FetchFromV2.CourseModels;

namespace LpApiIntegration.FetchFromV2.Db
{
    internal class DbWorker
    {
        public static void AddStudent(FullStudent apiStudent, LearnpointDbContext dbContext)
        {
            dbContext.Students.Add(
            new StudentModel()
            {
                ExternalId = apiStudent.Id,
                NationalRegistrationNumber = apiStudent.NationalRegistrationNumber,
                Username = apiStudent.Username,
                Email = apiStudent.Email,
                Email2 = apiStudent.Email2,
                MobilePhone = apiStudent.MobilePhone,
                HomePhone = apiStudent.HomePhone,
                FullName = apiStudent.FirstName + " " + apiStudent.LastName,
                IsActive = true
            });
        }

        public static void UpdateStudent(FullStudent apiStudent, LearnpointDbContext dbContext)
        {
            var dbStudents = dbContext.Students;
            var compareList = dbContext.Students.ToList();
            foreach (var dbStudent in dbStudents)
            {
                if (dbStudent.Id == apiStudent.Id)
                {
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
                    if (dbStudent.MobilePhone != apiStudent.MobilePhone)
                    {
                        dbStudent.MobilePhone = apiStudent.MobilePhone;
                    }
                    if (dbStudent.HomePhone != apiStudent.HomePhone)
                    {
                        dbStudent.HomePhone = apiStudent.HomePhone;
                    }
                    if (dbStudent.FullName != apiStudent.FirstName + " " + apiStudent.LastName)
                    {
                        dbStudent.FullName = apiStudent.FirstName + " " + apiStudent.LastName;
                    }
                }
                if (compareList.Any(s => s.Id == apiStudent.Id))
                {
                    dbStudent.IsActive = true;
                }
                else
                {
                    dbStudent.IsActive = false;
                }
            }
        }

        public static void AddCourse(IEnumerable<Group> apiGroups , IEnumerable<FullGroup> apiGroups2, IEnumerable<CourseDefinition> coursePoints, LearnpointDbContext dbContext)
        {
            foreach(var group in apiGroups)
            {
                dbContext.Courses.Add(
                new CourseModel()
                {
                    ExternalId = group.Id,
                    Name = group.Name,
                    Code = group.Code,
                    LifespanFrom = group.LifespanFrom,
                    LifespanUntil = group.LifespanUntil,
                    Points = null
                    
                });
            }

            foreach(var group in apiGroups2)
            {
                dbContext.Courses.Add(
                new CourseModel()
                {
                    ExternalId = group.Id,
                    Name = group.Name,
                    Code = group.Code,
                    LifespanFrom = group.LifespanFrom,
                    LifespanUntil = group.LifespanUntil,
                    Points = null

                });
            }

            //foreach (var course in coursePoints)
            //{
            //    if (course.Name == "Accounting")
            //    {
            //        var result = dbContext.Courses.Where(n => n.Name == "Accounting").First();

            //        result.Points = course.Points;
            //    }
            //    else if (course.Name == "Demenssjukdomar")
            //    {
            //        var result2 = dbContext.Courses.Where(n => n.Name == "Demenssjukdomar").First();

            //        result2.Points = course.Points;

            //    }

            //}

        }
    }
}
