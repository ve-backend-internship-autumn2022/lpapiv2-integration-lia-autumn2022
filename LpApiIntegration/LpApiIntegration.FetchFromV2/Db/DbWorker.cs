using LpApiIntegration.FetchFromV2.StudentModels;
using LpApiIntegration.FetchFromV2.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.Db
{
    internal class DbWorker
    {
        private static LearnpointDbContext DbContext = new();

        public static void AddStudents(StudentsApiResponse studentResponse)
        {
            foreach (var apiStudent in studentResponse.Data.Students)
            {
                if (!DbContext.Students.Any(s => s.Id == apiStudent.Id))
                {
                    DbContext.Students.Add(
                    new StudentModel()
                    {
                        Id = apiStudent.Id,
                        NationalRegistrationNumber = apiStudent.NationalRegistrationNumber,
                        Username = apiStudent.Username,
                        Email = apiStudent.Email,
                        Email2 = apiStudent.Email2,
                        MobilePhone = apiStudent.MobilePhone,
                        HomePhone = apiStudent.HomePhone,
                        FullName = apiStudent.FirstName + " " + apiStudent.LastName
                    });
                }
            }
            DbContext.SaveChanges();
        }
        public static void UpdateStudents(StudentsApiResponse studentResponse)
        {
            var apiStudents = studentResponse.Data.Students;
            foreach (var dbStudent in DbContext.Students)
            {
                foreach (var apiStudent in apiStudents)
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
                        //if (dbStudent.IsActive != apiStudent.HomePhone)
                        //{

                        //}
                    }
                }
            }
            DbContext.SaveChanges();
        }
    }
}
