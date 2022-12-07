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
            do
            {
                if (activeStudentsResponse.NextLink == null)
                {
                    DbManager.StudentManager(activeStudentsResponse);
                }
                else if (activeStudentsResponse.NextLink != null)
                {
                    DbManager.StudentManager(activeStudentsResponse);

                    string nextLinkRequest = FetchFromApi.GetNextLink(apiSettings, activeStudentsResponse.NextLink);
                    activeStudentsResponse = JsonSerializer.Deserialize<UserListApiResponse>(nextLinkRequest);

                    DbManager.StudentManager(activeStudentsResponse);
                }
            } while (activeStudentsResponse.NextLink != null);
        }
        public static void Courses(ApiSettings apiSettings, CourseDefinitionListApiResponse courseDefinitionResponse,
            CourseInstanceListApiResponse courseInstanceResponse)
        {
            do
            {
                if (courseDefinitionResponse.NextLink == null & courseInstanceResponse.NextLink == null)
                {
                    DbManager.CourseManager(courseDefinitionResponse, courseInstanceResponse);
                }
                else if (courseDefinitionResponse.NextLink != null & courseInstanceResponse.NextLink != null)
                {
                    DbManager.CourseManager(courseDefinitionResponse, courseInstanceResponse);

                    string nextLinkRequestDefinition = FetchFromApi.GetNextLink(apiSettings, courseDefinitionResponse.NextLink);
                    string nextLinkRequestInstance = FetchFromApi.GetNextLink(apiSettings, courseInstanceResponse.NextLink);
                    courseDefinitionResponse = JsonSerializer.Deserialize<CourseDefinitionListApiResponse>(nextLinkRequestDefinition);
                    courseInstanceResponse = JsonSerializer.Deserialize<CourseInstanceListApiResponse>(nextLinkRequestInstance);

                    DbManager.CourseManager(courseDefinitionResponse, courseInstanceResponse);
                }
            } while (courseDefinitionResponse.NextLink != null & courseInstanceResponse.NextLink != null);
        }
        public static void StaffMembers(ApiSettings apiSettings, UserListApiResponse activeStaffMembersResponse)
        {
            do
            {
                if (activeStaffMembersResponse.NextLink == null)
                {
                    DbManager.StaffManager(activeStaffMembersResponse);
                }
                else if (activeStaffMembersResponse.NextLink != null)
                {
                    DbManager.StaffManager(activeStaffMembersResponse);

                    string nextLinkRequest = FetchFromApi.GetNextLink(apiSettings, activeStaffMembersResponse.NextLink);
                    activeStaffMembersResponse = JsonSerializer.Deserialize<UserListApiResponse>(nextLinkRequest);

                    DbManager.StaffManager(activeStaffMembersResponse);
                }
            } while (activeStaffMembersResponse.NextLink != null);
        }
        public static void Programs(ApiSettings apiSettings, ProgramInstanceListApiResponse programInstanceResponse)
        {
            do
            {
                if (programInstanceResponse.NextLink == null)
                {
                    DbManager.ProgramManager(programInstanceResponse);
                }
                else if (programInstanceResponse.NextLink != null)
                {
                    DbManager.ProgramManager(programInstanceResponse);

                    string nextLinkRequest = FetchFromApi.GetNextLink(apiSettings, programInstanceResponse.NextLink);
                    programInstanceResponse = JsonSerializer.Deserialize<ProgramInstanceListApiResponse>(nextLinkRequest);

                    DbManager.ProgramManager(programInstanceResponse);
                }
            } while (programInstanceResponse.NextLink != null);
        }
    }
}
