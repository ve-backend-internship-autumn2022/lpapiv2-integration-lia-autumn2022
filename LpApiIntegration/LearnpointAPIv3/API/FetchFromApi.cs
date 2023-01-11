using LpApiIntegration.FetchFromV3.API.Models;
using System.Net;
using System.Text;
using System.Text.Json;

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

        public static List<CourseDefinition> GetCourseDefinitions(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/coursedefinitions";

            return FetchList<CourseDefinition>(apiSettings, apiRequestLink);

        }

        public static List<CourseEnrollment> GetCourseEnrollments(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/courseenrollments";

            return FetchList<CourseEnrollment>(apiSettings, apiRequestLink);
        }

        public static List<CourseInstance> GetCourseInstances(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/courseinstances?includePrevious=true&includeActive=true&includeFuture=true";

            return FetchList<CourseInstance>(apiSettings, apiRequestLink);
        }

        public static List<CourseStaffMembership> GetCourseStaffMembership(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/coursestaffmemberships";

            return FetchList<CourseStaffMembership>(apiSettings, apiRequestLink);
        }

        public static List<CourseGrade> GetCourseGrades(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/coursegrades";

            return FetchList<CourseGrade>(apiSettings, apiRequestLink);
        }

        public static List<User> GetActiveStudents(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/users?includeActive=true&includeInactive=false&includeStudents=true&includeStaff=false";

            return FetchList<User>(apiSettings, apiRequestLink);
        }

        public static List<User> GetActiveStaff(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/users?includeActive=true&includeInactive=false&includeStudents=false&includeStaff=true";

            return FetchList<User>(apiSettings, apiRequestLink);
        }

        public static List<ProgramInstance> GetProgramInstances(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/programinstances?includePrevious=true&includeActive=true&includeFuture=true&expandSpecializations=false";

            return FetchList<ProgramInstance>(apiSettings, apiRequestLink);
        }

        public static List<ProgramEnrollment> GetProgramEnrollments(ApiSettings apiSettings)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/programenrollments?includeActive=true&includeInactive=true&excludeCanceled=false&expandSelectedCourseDefinitions=false&expandSelectedSpecializations=true";

            return FetchList<ProgramEnrollment>(apiSettings, apiRequestLink);
        }

        public static List<User> GetUserLookup(ApiSettings apiSettings, int[] userIds)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/users/lookup";

            var content = new { Ids = userIds };

            return FetchList<User>(apiSettings, apiRequestLink, content);
        }

        public static List<CourseInstance> GetCourseInstancesLookup(ApiSettings apiSettings, int?[] courseInstanceIds)
        {
            var apiRequestLink = $"{apiSettings.ApiBaseAddress}/v3/{apiSettings.TenantIdentifier}/courseinstances/lookup";

            var content = new { Ids = courseInstanceIds };

            return FetchList<CourseInstance>(apiSettings, apiRequestLink, content);
        }

        private static List<T> FetchList<T>(ApiSettings apiSettings, string apiRequestLink, object content = null)
        {
            var list = new List<T>();
            do
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<T>>>(GetData(apiSettings, apiRequestLink, content));
                list.AddRange(apiResponse.Data);
                apiRequestLink = apiResponse.NextLink;
            } while (apiRequestLink != null);
            return list;
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