using System.Text.Json;
using System.Text.Json.Serialization;

namespace Demo.API.Services
{
    public class KeycloakAuthService : IKeycloakAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public KeycloakAuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<TokenResponse> LoginAsync(string username, string password)
        {
            var tokenEndpoint = $"{_configuration["Keycloak:BaseUrl"]}/realms/{_configuration["Keycloak:Realm"]}/protocol/openid-connect/token";

            var requestBody = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", _configuration["Keycloak:ClientId"] },
            { "client_secret", _configuration["Keycloak:ClientSecret"] },
            { "username", username },
            { "password", password }
        };

            var content = new FormUrlEncodedContent(requestBody);

            var response = await _httpClient.PostAsync(tokenEndpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResult = JsonSerializer.Deserialize<TokenResponse>(responseContent);

                return tokenResult!;
            }

            throw new Exception("Login to keycloak instance failed");
        }

        public async Task LogoutAsync(string refreshToken)
        {
            var logoutEndpoint = $"{_configuration["Keycloak:BaseUrl"]}/realms/{_configuration["Keycloak:Realm"]}/protocol/openid-connect/logout";

            var requestBody = new Dictionary<string, string>
            {
                { "client_id", _configuration["Keycloak:ClientId"] },
                { "client_secret", _configuration["Keycloak:ClientSecret"] },
                { "refresh_token", refreshToken }
            };

            var content = new FormUrlEncodedContent(requestBody);
            var response = await _httpClient.PostAsync(logoutEndpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Logout failed: {response.StatusCode} - {errorContent}");
            }
        }
    }

    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
