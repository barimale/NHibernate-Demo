using Demo.Domain.AggregatesModel.Company2Aggregate;
using Demo.Domain.AggregatesModel.CompanyAggregate;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.Infrastructure.Database;
using Demo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Demo.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddKeycloakAuthentication
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"{configuration["Keycloak:BaseUrl"]}/realms/{configuration["Keycloak:Realm"]}",
                    ValidateAudience = true,
                    ValidAudience = "account",
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,

                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        var client = new HttpClient();
                        var keyUri = $"{parameters.ValidIssuer}/protocol/openid-connect/certs";
                        var response = client.GetAsync(keyUri).Result;
                        var keys = new JsonWebKeySet(response.Content.ReadAsStringAsync().Result);

                        return keys.GetSigningKeys();
                    }
                };

                options.RequireHttpsMetadata = true; //WIP Only in develop environment
                options.SaveToken = true;
            });

        return services;
    }
}
