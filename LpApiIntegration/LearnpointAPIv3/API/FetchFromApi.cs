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
        private static HttpClient Client (ApiSettings apiSettings)
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

            var client = $"/v3/{apiSettings.TenantIdentifier}/coursedefinitions";

            return GetData(apiSettings, client);

            //return Client(apiSettings).GetStringAsync($"/v3/{apiSettings.TenantIdentifier}/coursedefinitions").Result;
        }

        public static string GetCourseEnrollments(ApiSettings apiSettings)
        {
            var client = $"/v3/{apiSettings.TenantIdentifier}/courseenrollments";

            return GetData(apiSettings, client);           
        }

        public static string GetCourseGrades(ApiSettings apiSettings)
        {
            var client = $"/v3/{apiSettings.TenantIdentifier}/coursegrades";

            return GetData(apiSettings, client);
            
        }

        public static string GetCourseInstances(ApiSettings apiSettings)
        {
            var client = $"/v3/{apiSettings.TenantIdentifier}/courseinstances?includePrevious=false&includeActive=true&includeFuture=true";

            return GetData(apiSettings, client);
            
        }

        public static string GetGroupMemberships(ApiSettings apiSettings)
        {
            var client = $"/v3/{apiSettings.TenantIdentifier}/groupmemberships";

            return GetData(apiSettings, client);
        }

        public static string GetGroups(ApiSettings apiSettings)
        {
            var client = $"/v3/{apiSettings.TenantIdentifier}/groups";

            return GetData(apiSettings, client);
        }

        public static string GetActiveStudents(ApiSettings apiSettings)
        {
            var client = $"/v3/{apiSettings.TenantIdentifier}/users?includeActive=true&includeInactive=false&includeStudents=true&includeStaff=false";

            return GetData(apiSettings, client);
        }

        public static string GetActiveStaff(ApiSettings apiSettings)
        {
            var client = $"/v3/{apiSettings.TenantIdentifier}/users?includeActive=true&includeInactive=false&includeStudents=false&includeStaff=true";

            return GetData(apiSettings, client);           
        }

        public static string GetProgramInstances(ApiSettings apiSettings)
        {
            var client = $"/v3/{apiSettings.TenantIdentifier}/programinstances?includePrevious=true&includeActive=true&includeFuture=true&expandSpecializations=false";

            return GetData(apiSettings, client);
        }

        public static string GetData(ApiSettings apiSettings, string client, object content = null)
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
                    responseTask = Client(apiSettings).GetAsync(client);
                }
                else
                {
                    var htmlContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
                    responseTask = Client(apiSettings).PostAsync(apiSettings.ApiBaseAddress, htmlContent);
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
                    Thread.Sleep(delay);
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
