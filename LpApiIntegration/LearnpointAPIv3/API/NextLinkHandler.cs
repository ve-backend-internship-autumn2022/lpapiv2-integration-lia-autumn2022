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

                    string nextLinkJsonActiveStudent = FetchFromApi.GetNextLink(apiSettings, activeStudentsResponse.NextLink);
                    activeStudentsResponse = JsonSerializer.Deserialize<UserListApiResponse>(nextLinkJsonActiveStudent);

                    DbManager.StudentManager(activeStudentsResponse);
                }
            } while (activeStudentsResponse.NextLink != null);
        }
    }
}
