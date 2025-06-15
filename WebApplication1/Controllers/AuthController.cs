using Demo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.API.Controllers
{
    /// <summary>
    /// API controller for authentication and authorization operations using Keycloak.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IKeycloakAuthService _keycloakAuthService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="keycloakAuthService">Service for Keycloak authentication operations.</param>
        public AuthController(IKeycloakAuthService keycloakAuthService)
        {
            _keycloakAuthService = keycloakAuthService;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token if successful.
        /// </summary>
        /// <param name="request">The login request containing username and password.</param>
        /// <returns>JWT token if authentication is successful; Unauthorized otherwise.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _keycloakAuthService.LoginAsync(request.Username, request.Password);
                return Ok(token);
            }
            catch (Exception ex)
            {
                // Return Unauthorized if authentication fails.
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <returns>Confirmation message upon successful logout.</returns>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            await _keycloakAuthService.LogoutAsync(request.RefreshToken);
            return Ok("Logged out successfully.");
        }

        /// <summary>
        /// Retrieves the roles of the currently authenticated user.
        /// </summary>
        /// <returns>A list of roles assigned to the user.</returns>
        [HttpGet("roles")]
        [Authorize]
        public IActionResult GetUserRoles()
        {
            var roles = User.Claims
                .Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)
                .Select(c => c.Value)
                .ToList();

            return Ok(new { Roles = roles });
        }

        public class LogoutRequest
        {
            /// <summary>
            /// Gets or sets the refresh token for logout.
            /// </summary>
            public string RefreshToken { get; set; }
        }

        /// <summary>
        /// Represents a login request with username and password.
        /// </summary>
        public class LoginRequest
        {
            /// <summary>
            /// Gets or sets the username for login.
            /// </summary>
            public string Username { get; set; }

            /// <summary>
            /// Gets or sets the password for login.
            /// </summary>
            public string Password { get; set; }
        }
    }
}
