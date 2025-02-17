using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Application;
using RegistrationWizard.Application.Models;
using RegistrationWizard.Application.Models.Validation;
using RegistrationWizard.Infrastructure;
using RegistrationWizard.Server.Extensions;
using RegistrationWizard.Server.Filters;

namespace RegistrationWizard.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<RegistrationWizardServerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RegistrationWizardServerContext") ?? throw new InvalidOperationException("Connection string 'RegistrationWizardServerContext' not found.")));


            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IDbContext, RegistrationWizardServerContext>();
            builder.Services.AddScoped<IApplicationFacade, ApplicationFacade>();

            builder.Services.AddValidatorsFromAssembly(typeof(RegistrationCommandValidator).Assembly);

            // Add services to the container.
            builder.Services.AddControllers(c =>
            {
                c.Filters.Add(new ValidationFilter<RegisterCommand>());
            });
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
