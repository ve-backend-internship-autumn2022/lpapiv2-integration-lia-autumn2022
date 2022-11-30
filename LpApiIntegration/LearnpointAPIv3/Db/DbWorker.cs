//using LpApiIntegration.Db;
//using LpApiIntegration.FetchFromV2.CourseModels;
//using LpApiIntegration.FetchFromV2.GroupModel;
//using LpApiIntegration.FetchFromV2.StaffMemberModels;
//using LpApiIntegration.FetchFromV2.StudentModels;

//namespace LpApiIntegration.FetchFromV2.Db
//{
//    internal class DbWorker
//    {
//        // Student
//        public static void AddStudent(FullStudent apiStudent, LearnpointDbContext dbContext)
//        {
//            dbContext.Students.Add(
//            new StudentModel()
//            {
//                ExternalId = apiStudent.Id,
//                ExternalUserId = apiStudent.UserId,
//                NationalRegistrationNumber = apiStudent.NationalRegistrationNumber,
//                Username = apiStudent.Username,
//                Email = apiStudent.Email,
//                Email2 = apiStudent.Email2,
//                MobilePhone = apiStudent.MobilePhone,
//                HomePhone = apiStudent.HomePhone,
//                FullName = apiStudent.FirstName + " " + apiStudent.LastName,
//                IsActive = true
//            });
//        }

//        public static void UpdateStudent(FullStudent apiStudent, LearnpointDbContext dbContext)
//        {
//            var dbStudent = dbContext.Students.Where(s => s.ExternalId == apiStudent.Id).SingleOrDefault();

//            if (dbStudent.NationalRegistrationNumber != apiStudent.NationalRegistrationNumber)
//            {
//                dbStudent.NationalRegistrationNumber = apiStudent.NationalRegistrationNumber;
//            }
//            if (dbStudent.Username != apiStudent.Username)
//            {
//                dbStudent.Username = apiStudent.Username;
//            }
//            if (dbStudent.Email != apiStudent.Email)
//            {
//                dbStudent.Email = apiStudent.Email;
//            }
//            if (dbStudent.Email2 != apiStudent.Email2)
//            {
//                dbStudent.Email2 = apiStudent.Email2;
//            }
//            if (dbStudent.MobilePhone != apiStudent.MobilePhone)
//            {
//                dbStudent.MobilePhone = apiStudent.MobilePhone;
//            }
//            if (dbStudent.HomePhone != apiStudent.HomePhone)
//            {
//                dbStudent.HomePhone = apiStudent.HomePhone;
//            }
//            if (dbStudent.FullName != apiStudent.FirstName + " " + apiStudent.LastName)
//            {
//                dbStudent.FullName = apiStudent.FirstName + " " + apiStudent.LastName;
//            }
//            if (dbStudent.IsActive == false)
//            {
//                dbStudent.IsActive = true;
//            }
//        }
//        public static void CheckForInactiveStudents(FullStudent[] students, LearnpointDbContext dbContext)
//        {
//            HashSet<int> diffids = new(students.Select(s => s.Id));
//            var results = dbContext.Students.Where(s => !diffids.Contains(s.ExternalId)).ToList();

//            foreach (var student in results)
//            {
//                student.IsActive = false;
//            }
//        }

//        // Course

//        public static void AddCourse(FullGroup apiCourse, IEnumerable<CourseDefinition> courseDefinitions, LearnpointDbContext dbContext)
//        {
//            dbContext.Courses.Add(
//                   new CourseModel()
//                   {
//                       ExternalId = apiCourse.Id,
//                       Name = apiCourse.Name,
//                       Code = apiCourse.Code,
//                       LifespanFrom = apiCourse.LifespanFrom,
//                       LifespanUntil = apiCourse.LifespanUntil,
//                       Points = courseDefinitions.Where(c => c.Id == apiCourse.CourseDefinition.Id).ToList().SingleOrDefault()?.Points
//                   });
//        }

//        public static void UpdateCourse(FullGroup apiCourse, IEnumerable<CourseDefinition> courseDefinitions, LearnpointDbContext dbContext)
//        {
//            var dbCourse = dbContext.Courses.Where(c => c.ExternalId == apiCourse.Id).SingleOrDefault();

//            if (dbCourse.Name != apiCourse.Name)
//            {
//                dbCourse.Name = apiCourse.Name;
//            }
//            if (dbCourse.Code != apiCourse.Code)
//            {
//                dbCourse.Code = apiCourse.Code;
//            }
//            if (dbCourse.LifespanFrom != apiCourse.LifespanFrom)
//            {
//                dbCourse.LifespanFrom = apiCourse.LifespanFrom;
//            }
//            if (dbCourse.LifespanUntil != apiCourse.LifespanUntil)
//            {
//                dbCourse.LifespanUntil = apiCourse.LifespanUntil;
//            }
//            if (dbCourse.Points != courseDefinitions.Where(c => c.Id == apiCourse.CourseDefinition.Id).ToList().SingleOrDefault()?.Points)
//            {
//                dbCourse.Points = courseDefinitions.Where(c => c.Id == apiCourse.CourseDefinition.Id).ToList().SingleOrDefault()?.Points;
//            }
//        }

//        // Staff

//        public static void AddStaffMember(FullStaffMember apiStaffMember, LearnpointDbContext dbContext)
//        {
//            dbContext.StaffMembers.Add(
//            new StaffModel()
//            {
//                ExternalId = apiStaffMember.Id,
//                ExternalUserId = apiStaffMember.UserId,                
//                NationalRegistrationNumber = apiStaffMember.NationalRegistrationNumber,
//                Signature = apiStaffMember.Signature,
//                FullName = apiStaffMember.FirstName + " " + apiStaffMember.LastName,
//                Username = apiStaffMember.Username,
//                Email = apiStaffMember.Email,
//                Email2 = apiStaffMember.Email2,
//                MobilePhone = apiStaffMember.MobilePhone,
//                MayExposeMobilePhoneToStudents = apiStaffMember.MayExposeMobilePhoneToStudents,
//                Phone2 = apiStaffMember.Phone2,
//                MayExposePhone2ToStudents = apiStaffMember.MayExposePhone2ToStudents,

//            });
//        }

//        public static void UpdateStaffMember(FullStaffMember apiStaffMember, LearnpointDbContext dbContext)
//        {
//            var dbStaff = dbContext.StaffMembers.Where(s => s.ExternalId == apiStaffMember.Id).SingleOrDefault();

//            if (dbStaff.NationalRegistrationNumber != apiStaffMember.NationalRegistrationNumber)
//            {
//                dbStaff.NationalRegistrationNumber = apiStaffMember.NationalRegistrationNumber;
//            }
//            if (dbStaff.Signature != apiStaffMember.Signature)
//            {
//                dbStaff.Signature = apiStaffMember.Signature;
//            }
//            if (dbStaff.FullName != apiStaffMember.FirstName + " " + apiStaffMember.LastName)
//            {
//                dbStaff.FullName = apiStaffMember.FirstName + " " + apiStaffMember.LastName;
//            }
//            if (dbStaff.Username != apiStaffMember.Username)
//            {
//                dbStaff.Username = apiStaffMember.Username;
//            }
//            if (dbStaff.Email != apiStaffMember.Email)
//            {
//                dbStaff.Email = apiStaffMember.Email;
//            }
//            if (dbStaff.Email2 != apiStaffMember.Email2)
//            {
//                dbStaff.Email2 = apiStaffMember.Email2;
//            }
//            if (dbStaff.MobilePhone != apiStaffMember.MobilePhone)
//            {
//                dbStaff.MobilePhone = apiStaffMember.MobilePhone;
//            }
//            if (dbStaff.MayExposeMobilePhoneToStudents != apiStaffMember.MayExposeMobilePhoneToStudents)
//            {
//                dbStaff.MayExposeMobilePhoneToStudents = apiStaffMember.MayExposeMobilePhoneToStudents;
//            }
//            if (dbStaff.Phone2 != apiStaffMember.Phone2)
//            {
//                dbStaff.Phone2 = apiStaffMember.Phone2;
//            }
//            if (dbStaff.MayExposePhone2ToStudents != apiStaffMember.MayExposePhone2ToStudents)
//            {
//                dbStaff.MayExposePhone2ToStudents = apiStaffMember.MayExposePhone2ToStudents;
//            }
//        }

//        // Program

//        public static void AddProgram(FullGroup apiProgram, LearnpointDbContext dbContext)
//        {
//            dbContext.Programs.Add(
//                new ProgramModel()
//                {
//                    ExternalId = apiProgram.Id,
//                    Code = apiProgram.Code,
//                    Name = apiProgram.Name,
//                    LifespanFrom = apiProgram.LifespanFrom,
//                    LifespanUntil = apiProgram.LifespanUntil
//                });
//        }
//        public static void UpdateProgram(FullGroup apiProgram, LearnpointDbContext dbContext)
//        {
//            var dbProgram = dbContext.Programs.Where(p => p.ExternalId == apiProgram.Id).SingleOrDefault();

//            if (dbProgram.Code != apiProgram.Code)
//            {
//                dbProgram.Code = apiProgram.Code;
//            }
//            if (dbProgram.Name != apiProgram.Name)
//            {
//                dbProgram.Name = apiProgram.Name;
//            }
//            if (dbProgram.LifespanFrom != apiProgram.LifespanFrom)
//            {
//                dbProgram.LifespanFrom = apiProgram.LifespanFrom;
//            }
//            if (dbProgram.LifespanUntil != apiProgram.LifespanUntil)
//            {
//                dbProgram.LifespanUntil = apiProgram.LifespanUntil;
//            }
//        }

//        // Relations
//        public static void AddCourseStudentRelation(StudentCourseRelationModel relation, LearnpointDbContext dbContext)
//        {
//            dbContext.StudentCourseRelations.Add(relation);
//        }
//        public static void AddCourseStaffRelation(StaffCourseRelationModel relation, LearnpointDbContext dbContext)
//        {
//            dbContext.StaffCourseRelations.Add(relation);
//        }
//        public static void AddStudentProgramRelation(StudentProgramRelationModel relation, LearnpointDbContext dbContext)
//        {
//            dbContext.StudentProgramRelations.Add(relation);
//        }

//        public static void RemoveCourseStudentRelation(StudentCourseRelationModel relation, LearnpointDbContext dbContext)
//        {
//            dbContext.StudentCourseRelations.Remove(relation);
//        }
//        public static void RemoveCourseStaffRelation(StaffCourseRelationModel relation, LearnpointDbContext dbContext)
//        {
//            dbContext.StaffCourseRelations.Remove(relation);
//        }
//        public static void RemoveStudentProgramRelation(StudentProgramRelationModel relation, LearnpointDbContext dbContext)
//        {
//            dbContext.StudentProgramRelations.Remove(relation);
//        }
//    }
//}


    

