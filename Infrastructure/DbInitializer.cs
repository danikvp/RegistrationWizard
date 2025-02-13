using RegistrationWizard.Domain.Entities;

namespace RegistrationWizard.Infrastructure
{

    public class DbInitializer
    {
        public static void Initialize(RegistrationWizardServerContext context)
        {

            context.Database.EnsureCreated();

            if (!context.Country.Any())
            {
                context.Country.AddRange(
                    new Country { Name = "Country 1" },
                    new Country { Name = "Country 2" }
                    );
            }

            context.SaveChanges();

            if (!context.Province.Any())
            {
                context.Province.AddRange(

                    new Province { Name = "Province 1.1", Country = context.Country.First(c => c.Name == "Country 1") },
                    new Province { Name = "Province 1.2", Country = context.Country.First(c => c.Name == "Country 1") },

                    new Province { Name = "Province 2.1", Country = context.Country.First(c => c.Name == "Country 2") },
                    new Province { Name = "Province 2.2", Country = context.Country.First(c => c.Name == "Country 2") }

                    );
            }

            context.SaveChanges();
        }
    }
}
