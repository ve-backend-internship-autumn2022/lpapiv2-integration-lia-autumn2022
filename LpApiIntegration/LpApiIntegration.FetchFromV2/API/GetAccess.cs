using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.API
{
    internal class GetAccess
    {
        public static string Token(ApiSettings apiSettings)
        {
            //Get Access token
            var tokenHttpClient = new HttpClient();
            var credentials = $"{apiSettings.ClientId}:{apiSettings.ClientSecret}";
            var base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            tokenHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
            var tokenResponse = tokenHttpClient.PostAsync(apiSettings.TokenEndpointUri,
                                                            new FormUrlEncodedContent(new Dictionary<string, string>{
                                                                { "grant_type", "client_credentials" },
                                                                { "scope", apiSettings.RequestedScopes }
                                                                })).Result;
            var tokenResponseContent = tokenResponse.Content.ReadAsStringAsync().Result;

            //Construct a JSON document from the response and extract the AccessToken
            var tokenResponseJSON = JsonDocument.Parse(tokenResponseContent);
            string accessToken = null;
            if (tokenResponseJSON.RootElement.TryGetProperty("access_token", out JsonElement accessTokenJSONValue))
            {
                accessToken = accessTokenJSONValue.ToString();
            }
            return accessToken;
        }
    }
}
