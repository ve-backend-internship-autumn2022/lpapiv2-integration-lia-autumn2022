using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LearnpointAPIv3.API
{
    internal class FetchFromApi
    {
        private static HttpClient Client(ApiSettings apiSettings)
        {
            //Creates API Client
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetAccess.Token(apiSettings));
            return client;
        }

        public static string GetCourseDefinitions(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/coursedefinitions";

            return GetData(apiSettings, apiRequestLink, null);
        }

        public static string GetCourseEnrollments(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/courseenrollments";

            return GetData(apiSettings, apiRequestLink, null);           
        }

        public static string GetCourseGrades(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/coursegrades";

            return GetData(apiSettings, apiRequestLink, null);
        }

        public static string GetCourseInstances(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/courseinstances?includePrevious=false&includeActive=true&includeFuture=true";

            return GetData(apiSettings, apiRequestLink, null);
        }

        public static string GetGroupMemberships(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/groupmemberships";

            return GetData(apiSettings, apiRequestLink, null);
        }

        public static string GetCourseStaffMembership(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/coursestaffmemberships";

            return GetData(apiSettings, apiRequestLink, null);
        }

        public static string GetGroups(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/groups";

            return GetData(apiSettings, apiRequestLink, null);
        }

        public static string GetStudents(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/users?includeActive=true&includeInactive=false&includeStudents=true&includeStaff=false";
            
            return GetData(apiSettings, apiRequestLink, null);
        }

        public static string GetStudent(ApiSettings apiSettings, int userId)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/users/{userId}";

            return GetData(apiSettings, apiRequestLink, null);
        }

        public static string GetActiveStaff(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/users?includeActive=true&includeInactive=false&includeStudents=false&includeStaff=true";

            return GetData(apiSettings, apiRequestLink, null);
        }

        public static string GetProgramInstances(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/programinstances?includePrevious=true&includeActive=true&includeFuture=true&expandSpecializations=false";

            return GetData(apiSettings, apiRequestLink, null);
        }

        public static string GetProgramEnrollments(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/programenrollments?includeActive=true&includeInactive=true&excludeCanceled=true&expandSelectedCourseDefinitions=false&expandSelectedSpecializations=false";

            return GetData(apiSettings, apiRequestLink, null);
        }

        public static string GetEnrollmentStudents(ApiSettings apiSettings, int[] lookupFilter)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/users/lookup";

            var content = new { Ids = lookupFilter };
            
            return GetData(apiSettings, apiRequestLink, content);
        }

        private static string GetData(ApiSettings apiSettings, string apiRequestLink, object content = null)
        {
            int defaultRetrySeconds = 10;
            int maxDelaySeconds = 3600;
            int maxRetry = 3;
            int retryCount = 0;

            while (retryCount < maxRetry)
            {
                retryCount++;
                Task<HttpResponseMessage> responseTask;
                if (content == null)
                {
                    responseTask = Client(apiSettings).GetAsync(apiRequestLink);
                }
                else 
                {
                    var htmlContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
                    responseTask = Client(apiSettings).PostAsync(apiRequestLink, htmlContent);
                }

                HttpResponseMessage response = responseTask.Result;   
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else if (response.StatusCode == (HttpStatusCode)429)
                {
                    TimeSpan delay = TimeSpan.FromSeconds(defaultRetrySeconds);
                    if (response.Headers.TryGetValues("Retry-After", out IEnumerable<string> values))
                    {
                        string retryAfter = values.First();
                        if (Int32.TryParse(retryAfter, out int delaySeconds))
                        {
                            delay = TimeSpan.FromSeconds(delaySeconds > maxDelaySeconds ? maxDelaySeconds : delaySeconds);
                        }
                    }
                    Console.WriteLine($"Error! Too many Requests, wait {delay.Seconds} seconds");
                    Thread.Sleep(delay);
                    Console.WriteLine("Thank you for waiting!");
                }
                else
                {
                    throw (new ApplicationException($"Status code: {response.StatusCode}. Reason: {response.ReasonPhrase}. Content: {response.Content.ReadAsStringAsync().Result}"));
                }
            }
            throw (new ApplicationException($"Too many retries"));
        }
    }
}