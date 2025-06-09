using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.Infrastructure;
using FluentValidation.AspNetCore;

namespace Demo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services
                .AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>());

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.EnableAnnotations();
            });

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }
            // Configure the HTTP request pipeline.

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
