using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnpointAPIv3.API
{
    internal class FetchFromApi
    {
        private static HttpClient Client(ApiSettings apiSettings)
        {
            //Creates API Client
            var client = new HttpClient
            {
                BaseAddress = new Uri(apiSettings.ApiBaseAddress)
            };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetAccess.Token(apiSettings));
            return client;
        }

        public static string GetCourseDefinitions(ApiSettings apiSettings)
        {
            return Client(apiSettings).GetStringAsync($"/v3/{apiSettings.TenantIdentifier}/coursedefinitions").Result;
        }

        public static string GetCourseEnrollments(ApiSettings apiSettings)
        {
            return Client(apiSettings).GetStringAsync($"/v3/{apiSettings.TenantIdentifier}/courseenrollments").Result;
        }

        public static string GetCourseGrades(ApiSettings apiSettings)
        {
            return Client(apiSettings).GetStringAsync($"/v3/{apiSettings.TenantIdentifier}/coursegrades").Result;
        }

        public static string GetCourseInstances(ApiSettings apiSettings)
        {
            return Client(apiSettings).GetStringAsync($"/v3/{apiSettings.TenantIdentifier}/courseinstances?includePrevious=false&includeActive=true&includeFuture=true").Result;
        }

        public static string GetGroupMemberships(ApiSettings apiSettings)
        {
            return Client(apiSettings).GetStringAsync($"/v3/{apiSettings.TenantIdentifier}/groupmemberships").Result;
        }

        public static string GetGroups(ApiSettings apiSettings)
        {
            return Client(apiSettings).GetStringAsync($"/v3/{apiSettings.TenantIdentifier}/groups").Result;
        }
        public static string GetActiveStudents(ApiSettings apiSettings)
        {
            return Client(apiSettings).GetStringAsync($"/v3/{apiSettings.TenantIdentifier}/users?includeActive=true&includeInactive=false&includeStudents=true&includeStaff=false&limit=20").Result;
        }

        public static string GetActiveStaff(ApiSettings apiSettings)
        {
            return Client(apiSettings).GetStringAsync($"/v3/{apiSettings.TenantIdentifier}/users?includeActive=true&includeInactive=false&includeStudents=false&includeStaff=true").Result;
        }

        public static string GetNextLink(ApiSettings apiSettings, string nextLink)
        {
            return Client(apiSettings).GetStringAsync(nextLink.Remove(0, 46)).Result;
        }
    }
}
