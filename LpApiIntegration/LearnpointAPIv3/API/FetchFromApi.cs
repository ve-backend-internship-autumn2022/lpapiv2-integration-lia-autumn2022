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

        public static string GetCoursedefinitions(ApiSettings apiSettings)
        {
            return Client(apiSettings).GetStringAsync($"/v3/{apiSettings.TenantIdentifier}/coursedefinitions").Result;
        }

        //public static string GetStudents(ApiSettings apiSettings)
        //{
        //    return Client(apiSettings).GetStringAsync($"/bulkapi/v2/{apiSettings.TenantIdentifier}/students").Result;
        //}
        //public static string GetStudentsExtended(ApiSettings apiSettings)
        //{
        //    return Client(apiSettings).GetStringAsync($"/bulkapi/v2/{apiSettings.TenantIdentifier}/students?hasactiveeducation=false").Result;
        //}

        //public static string GetGroups(ApiSettings apiSettings)
        //{
        //    return Client(apiSettings).GetStringAsync($"/bulkapi/v2/{apiSettings.TenantIdentifier}/groups").Result;
        //}
        //public static string GetGroupsExtended(ApiSettings apiSettings)
        //{
        //    return Client(apiSettings).GetStringAsync($"/bulkapi/v2/{apiSettings.TenantIdentifier}/groups?includecurrentandfuturegroupsonly=false").Result;
        //}

        //public static string GetStaffMembers(ApiSettings apiSettings)
        //{
        //    return Client(apiSettings).GetStringAsync($"/bulkapi/v2/{apiSettings.TenantIdentifier}/staffmembers").Result;
        //}

    }
}
