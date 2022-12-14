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
        public static void Students(ApiSettings apiSettings, UserListApiResponse activeStudentsResponse)
        {
            var students = new List<User>();

            do
            {
                if (activeStudentsResponse.NextLink == null)
                {
                    students.AddRange(activeStudentsResponse.Data);
                }
                else if (activeStudentsResponse.NextLink != null)
                {
                    students.AddRange(activeStudentsResponse.Data);

                    activeStudentsResponse = JsonSerializer.Deserialize<UserListApiResponse>(activeStudentsResponse.NextLink);

                    students.AddRange(activeStudentsResponse.Data);
                }
            } while (activeStudentsResponse.NextLink != null);

            DbManager.StudentManager(students);
        }
        public static void Courses(ApiSettings apiSettings, CourseDefinitionListApiResponse courseDefinitionResponse,
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
        public static void StaffMembers(ApiSettings apiSettings, UserListApiResponse activeStaffMembersResponse)
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
        public static void Programs(ApiSettings apiSettings, ProgramInstanceListApiResponse programInstanceResponse)
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
        public static void Relations(ApiSettings apiSettings, CourseStaffMembershipListApiResponse courseStaffMembershipResponse, CourseInstanceListApiResponse courseInstanceResponse)
        {
            var courseStaffReletions = new List<CourseStaffMembership>();
            var courseInstances = new List<CourseInstance>();

            do
            {
                if (courseStaffMembershipResponse.NextLink == null)
                {
                    courseStaffReletions.AddRange(courseStaffMembershipResponse.Data);
                }
                else if (courseStaffMembershipResponse.NextLink != null)
                {
                    courseStaffReletions.AddRange(courseStaffMembershipResponse.Data);

                    courseStaffMembershipResponse = JsonSerializer.Deserialize<CourseStaffMembershipListApiResponse>(courseStaffMembershipResponse.NextLink);

                    courseStaffReletions.AddRange(courseStaffMembershipResponse.Data);
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

            DbManager.RelationshipManager(courseStaffReletions, courseInstances);
        }
    }
}
