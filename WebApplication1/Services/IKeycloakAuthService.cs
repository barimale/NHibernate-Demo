
namespace Demo.API.Services
{
    public interface IKeycloakAuthService
    {
        Task<TokenResponse> LoginAsync(string username, string password);
        Task LogoutAsync(string refreshToken);
    }
}