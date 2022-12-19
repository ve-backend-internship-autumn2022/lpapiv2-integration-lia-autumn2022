using LearnpointAPIv3;
using LearnpointAPIv3.API;
using LpApiIntegration.FetchFromV2.Db;
using LpApiIntegration.FetchFromV3.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV3.API
{
    internal class NextLinkHandler
    {
        public static void Students(UserListApiResponse studentsResponse)
        {
            var students = new List<User>();

            do
            {
                if (studentsResponse.NextLink == null)
                {
                    students.AddRange(studentsResponse.Data);
                }
                else if (studentsResponse.NextLink != null)
                {
                    students.AddRange(studentsResponse.Data);

                    studentsResponse = JsonSerializer.Deserialize<UserListApiResponse>(studentsResponse.NextLink);

                    students.AddRange(studentsResponse.Data);
                }
            } while (studentsResponse.NextLink != null);

            DbManager.StudentManager(students, null);
        }
        public static void Courses(CourseDefinitionListApiResponse courseDefinitionResponse,
            CourseInstanceListApiResponse courseInstanceResponse)
        {
            var courseDefinitions = new List<CourseDefinition>();
            var courseInstances = new List<CourseInstance>();

            do
            {
                if (courseDefinitionResponse.NextLink == null & courseInstanceResponse.NextLink == null)
                {
                    courseDefinitions.AddRange(courseDefinitionResponse.Data);
                    courseInstances.AddRange(courseInstanceResponse.Data);
                }
                else if (courseDefinitionResponse.NextLink != null & courseInstanceResponse.NextLink != null)
                {
                    courseDefinitions.AddRange(courseDefinitionResponse.Data);
                    courseInstances.AddRange(courseInstanceResponse.Data);

                    courseDefinitionResponse = JsonSerializer.Deserialize<CourseDefinitionListApiResponse>(courseDefinitionResponse.NextLink);
                    courseInstanceResponse = JsonSerializer.Deserialize<CourseInstanceListApiResponse>(courseInstanceResponse.NextLink);

                    courseDefinitions.AddRange(courseDefinitionResponse.Data);
                    courseInstances.AddRange(courseInstanceResponse.Data);
                }
            } while (courseDefinitionResponse.NextLink != null & courseInstanceResponse.NextLink != null);

            DbManager.CourseManager(courseDefinitions, courseInstances);
        }
        public static void StaffMembers(UserListApiResponse activeStaffMembersResponse)
        {
            var staffMembers = new List<User>();

            do
            {
                if (activeStaffMembersResponse.NextLink == null)
                {
                    staffMembers.AddRange(activeStaffMembersResponse.Data);
                }
                else if (activeStaffMembersResponse.NextLink != null)
                {
                    staffMembers.AddRange(activeStaffMembersResponse.Data);

                    activeStaffMembersResponse = JsonSerializer.Deserialize<UserListApiResponse>(activeStaffMembersResponse.NextLink);

                    staffMembers.AddRange(activeStaffMembersResponse.Data);
                }
            } while (activeStaffMembersResponse.NextLink != null);

            DbManager.StaffManager(staffMembers);
        }
        public static void Programs(ProgramInstanceListApiResponse programInstanceResponse)
        {
            var programs = new List<ProgramInstance>();

            do
            {
                if (programInstanceResponse.NextLink == null)
                {
                    programs.AddRange(programInstanceResponse.Data);
                }
                else if (programInstanceResponse.NextLink != null)
                {
                    programs.AddRange(programInstanceResponse.Data);

                    programInstanceResponse = JsonSerializer.Deserialize<ProgramInstanceListApiResponse>(programInstanceResponse.NextLink);

                    programs.AddRange(programInstanceResponse.Data);
                }
            } while (programInstanceResponse.NextLink != null);

            DbManager.ProgramManager(programs);
        }
        public static void Relations(CourseStaffMembershipListApiResponse courseStaffMembershipResponse, CourseInstanceListApiResponse courseInstanceResponse, GroupMembershipListApiResponse groupMembershipResponse, CourseEnrollmentListApiResponse courseEnrollmentResponse, ProgramEnrollmentListApiResponse programEnrollmentResponse, ApiSettings apiSettings)
        {
            var courseStaffRelations = new List<CourseStaffMembership>();
            var courseInstances = new List<CourseInstance>();
            var groupMemberships = new List<GroupMembership>();
            var courseEnrollments = new List<CourseEnrollment>();
            var programEnrollments = new List<ProgramEnrollment>();

            do
            {
                if (courseStaffMembershipResponse.NextLink == null)
                {
                    courseStaffRelations.AddRange(courseStaffMembershipResponse.Data);
                }
                else if (courseStaffMembershipResponse.NextLink != null)
                {
                    courseStaffRelations.AddRange(courseStaffMembershipResponse.Data);

                    courseStaffMembershipResponse = JsonSerializer.Deserialize<CourseStaffMembershipListApiResponse>(courseStaffMembershipResponse.NextLink);

                    courseStaffRelations.AddRange(courseStaffMembershipResponse.Data);
                }
            } while (courseStaffMembershipResponse.NextLink != null);

            do
            {
                if (courseInstanceResponse.NextLink == null)
                {
                    courseInstances.AddRange(courseInstanceResponse.Data);
                }
                else if (courseInstanceResponse.NextLink != null)
                {
                    courseInstances.AddRange(courseInstanceResponse.Data);

                    courseInstanceResponse = JsonSerializer.Deserialize<CourseInstanceListApiResponse>(courseInstanceResponse.NextLink);

                    courseInstances.AddRange(courseInstanceResponse.Data);
                }
            } while (courseInstanceResponse.NextLink != null);           

            do
            {
                if (courseEnrollmentResponse.NextLink == null)
                {
                    courseEnrollments.AddRange(courseEnrollmentResponse.Data);
                }
                else if (courseEnrollmentResponse.NextLink != null)
                {
                    courseEnrollments.AddRange(courseEnrollmentResponse.Data);

                    courseEnrollmentResponse = JsonSerializer.Deserialize<CourseEnrollmentListApiResponse>(courseEnrollmentResponse.NextLink);

                    courseEnrollments.AddRange(courseEnrollmentResponse.Data);
                }
            } while (courseInstanceResponse.NextLink != null);

            do
            {
                if (programEnrollmentResponse.NextLink == null)
                {
                    programEnrollments.AddRange(programEnrollmentResponse.Data);
                }
                else if (programEnrollmentResponse.NextLink != null)
                {
                    programEnrollments.AddRange(programEnrollmentResponse.Data);

                    programEnrollmentResponse = JsonSerializer.Deserialize<ProgramEnrollmentListApiResponse>(programEnrollmentResponse.NextLink);

                    programEnrollments.AddRange(programEnrollmentResponse.Data);
                }
            } while (courseInstanceResponse.NextLink != null);

            DbManager.RelationshipManager(courseStaffRelations, courseInstances, courseEnrollments, programEnrollments, apiSettings);
        }
    }
}
