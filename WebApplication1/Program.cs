using Demo.API.DTOs;
using Demo.API.DTOs.Profiles;
using Demo.API.Middlewares.GlobalExceptions.Handler;
using Demo.API.Services;
using Demo.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

            // Specify the assembly explicitly to resolve ambiguity
            builder.Services.AddAutoMapper(cfg => { 
                cfg.AddProfile<MappingProfile>(); });

            builder.Services
                .AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>());

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
            });

            builder.Services.AddKeycloakAuthentication(builder.Configuration);

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<KeycloakAuthService>();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var app = builder.Build();

            app.UseExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

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
