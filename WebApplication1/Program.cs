using Demo.API.DTOs;
using Demo.API.DTOs.Profiles;
using Demo.API.Middlewares.GlobalExceptions.Handler;
using Demo.API.Services;
using Demo.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace Demo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddProblemDetails(options =>
                options.CustomizeProblemDetails = ctx =>
                    ctx.ProblemDetails.Extensions.Add("nodeId", Environment.MachineName));

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IKeycloakAuthService, KeycloakAuthService>();

            builder.Services.AddAutoMapper(cfg => { 
                cfg.AddProfile<MappingProfile>(); });

            builder.Services
                .AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>());

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                // Define the OAuth2 security scheme using implicit flow
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{builder.Configuration["Keycloak:BaseUrl"]}/realms/{builder.Configuration["Keycloak:Realm"]}/protocol/openid-connect/auth"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "openid scope for authentication" }
                            }
                        }
                    }
                });

                // Require OAuth2 security scheme globally
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        },
                        new List<string> { "openid" }
                    }
                });
            });

            builder.Services.AddKeycloakAuthentication(builder.Configuration);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var app = builder.Build();

            app.UseExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    // Configure Swagger UI to use the OAuth2 settings
                    options.OAuthClientId(builder.Configuration["Keycloak:ClientId"]);
                    options.OAuthAppName("Swagger UI - Keycloak Integration");
                    // Uncomment the following line if using Authorization Code with PKCE
                    // options.OAuthUsePkce();
                });

            }
            // Configure the HTTP request pipeline.

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
