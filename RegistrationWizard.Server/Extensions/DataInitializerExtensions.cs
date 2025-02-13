using RegistrationWizard.Infrastructure;

namespace RegistrationWizard.Server.Extensions
{
    public static class DataInitializerExtensions
    {
        public static void InitDataBase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<RegistrationWizardServerContext>();
            DbInitializer.Initialize(context);
        }
    }
}
