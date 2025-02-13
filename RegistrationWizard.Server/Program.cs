using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Application;
using RegistrationWizard.Domain;
using RegistrationWizard.Infrastructure;
using RegistrationWizard.Server.Extensions;

namespace RegistrationWizard.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<RegistrationWizardServerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RegistrationWizardServerContext") ?? throw new InvalidOperationException("Connection string 'RegistrationWizardServerContext' not found.")));


            builder.Services.AddScoped<IDbContext, RegistrationWizardServerContext>();
            builder.Services.AddScoped<IApplicationFacade, ApplicationFacade>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.InitDataBase();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
