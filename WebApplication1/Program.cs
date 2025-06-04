using NHibernateTestBlog;
using WebApplication1.Repositories.DbContext;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<INHibernateHelper, NHibernateHelper>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
